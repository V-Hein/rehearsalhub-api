
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/instrument_types")]
public class InstrumentTypesController : ControllerBase
{
    private readonly AppDbContext _db;

    public InstrumentTypesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InstrumentType>>> GetAll()
    {
        var instrumentTypes = await _db.InstrumentTypes
            .Select(it => new InstrumentTypeDto(
                it.Id,
                it.Name
            )).ToListAsync();

        if (instrumentTypes == null)
            return NotFound();

        return Ok(instrumentTypes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InstrumentType>> GetById(int id)
    {
        var instrumentType = await _db.InstrumentTypes
            .Where(it => it.Id == id)
            .Select(it => new InstrumentTypeDto(
                it.Id,
                it.Name
            )).FirstOrDefaultAsync();

        if (instrumentType == null)
            return NotFound();

        return Ok(instrumentType);
    }

    [HttpPost]
    public async Task<ActionResult<InstrumentType>> Create(CreateInstrumentTypeDto dto)
    {
        var instrumentType = new InstrumentType
        {
            Name = dto.Name
        };

        _db.InstrumentTypes.Add(instrumentType);

        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = instrumentType.Id }, instrumentType);
    }
}