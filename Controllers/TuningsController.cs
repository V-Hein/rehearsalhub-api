

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/tunings")]
public class TuningsController : ControllerBase
{
    private readonly AppDbContext _db;

    public TuningsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tuning>>> GetAll()
    {
        var tunings = await _db.Tunings
            .Include(t => t.Instrument)
            .Select(t => new TuningDto(
                t.Id,
                t.Name,
                t.Notes,
                t.Instrument.Name
            )).ToListAsync();

        return Ok(tunings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tuning>> GetById(int id)
    {        
        var tuning = await _db.Tunings
            .Where(t => t.Id == id)
            .Include(t => t.Instrument)
            .Select(t => new TuningDto(
                t.Id,
                t.Name,
                t.Notes,
                t.Instrument.Name
            )).FirstOrDefaultAsync();

        if (tuning == null)
            return NotFound();

        return Ok(tuning);
    }

    [HttpPost]
    public async Task<ActionResult<Tuning>> Create(CreateTuningDto dto)
    {
        var tuning = new Tuning
        {
            Name = dto.Name,
            Notes = dto.Notes,
            InstrumentId = dto.InstrumentId
        };

        _db.Tunings.Add(tuning);

        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = tuning.Id }, tuning);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tuning = await _db.Tunings.FindAsync(id);

        if (tuning == null)
            return NotFound();

        _db.Tunings.Remove(tuning);

        await _db.SaveChangesAsync();

        return NoContent();
    }
}