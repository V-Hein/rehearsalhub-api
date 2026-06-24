

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/tunings")]
[Authorize]
public class TuningsController : ControllerBase
{
    private readonly AppDbContext _db;
    public TuningsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TuningDto>>> GetAll()
    {
        var tunings = await GetTunings();
        return Ok(tunings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TuningDto>> GetById(int id)
    {        
        var tuning = await GetTuning(id);
        return tuning == null ? NotFound() : Ok(tuning);
    }

    [HttpPost]
    public async Task<ActionResult<TuningDto>> Create(CreateTuningDto dto)
    {
        var tuning = new Tuning
        {
            Name = dto.Name,
            Notes = dto.Notes,
        };

        _db.Tunings.Add(tuning);
        await _db.SaveChangesAsync();

        var result = await GetTuning(tuning.Id);

        return CreatedAtAction(nameof(GetById), new { id = tuning.Id }, result);
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

    private async Task<List<TuningDto>> GetTunings() =>
        await ToDto(BaseQuery()).ToListAsync();

    private async Task<TuningDto?> GetTuning(int id) =>
        await ToDto(BaseQuery().Where(t => t.Id == id)).FirstOrDefaultAsync();

    private IQueryable<TuningDto> ToDto(IQueryable<Tuning> query) =>
        query
            .OrderBy(t => t.Id)
            .Select(t => new TuningDto(
                t.Id,
                t.Name,
                t.Notes
            ));

    private IQueryable<Tuning> BaseQuery() => _db.Tunings.AsNoTracking();
}