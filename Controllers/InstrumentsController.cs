
using System.Runtime.InteropServices.Marshalling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/instruments")]
public class InstrumentsController : ControllerBase
{
    private readonly AppDbContext _db;

    public InstrumentsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Instrument>>> GetAll()
    {
        var instruments = await _db.Instruments
            .Include(i => i.InstrumentType)
            .Select(i => new InstrumentDto(
                i.Id,
                i.Name,
                new InstrumentTypeDto(
                    i.InstrumentType.Id,
                    i.InstrumentType.Name
                )
            )).ToListAsync();

        if (instruments == null)
            return NotFound();

        return Ok(instruments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Instrument>> GetById(int id)
    {
        var instrument = await _db.Instruments
            .Where(i => i.Id == id)
            .Include(i => i.InstrumentType)
            .Select(i => new InstrumentDto(
                i.Id,
                i.Name,
                new InstrumentTypeDto(
                    i.InstrumentType.Id,
                    i.InstrumentType.Name
                )
            )).FirstOrDefaultAsync();

        if (instrument == null)
            return NotFound();

        return Ok(instrument);
    }

    [HttpPost]
    public async Task <ActionResult<Instrument>> Create(CreateInstrumentDto dto)
    {
        var instrument = new Instrument
        {
            Name = dto.Name,
            InstrumentTypeId = dto.TypeId
        };

        _db.Instruments.Add(instrument);
        await _db.SaveChangesAsync();

        var result = await _db.Instruments
            .Where(i => i.Id == instrument.Id)
            .Include(i => i.InstrumentType)
            .Select(i => new InstrumentDto(
                i.Id,
                i.Name,
                new InstrumentTypeDto(
                    i.InstrumentType.Id,
                    i.InstrumentType.Name
                )
            )).FirstOrDefaultAsync();

        return CreatedAtAction(nameof(GetById), new { id = result!.Id }, result);
    }
}