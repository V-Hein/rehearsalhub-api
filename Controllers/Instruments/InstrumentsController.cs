
using System.Runtime.InteropServices.Marshalling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/instruments")]
[Authorize]
public class InstrumentsController : ControllerBase
{
    private readonly AppDbContext _db;
    public InstrumentsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InstrumentDto>>> GetAll()
    {
        var instruments = await GetInstruments();
        return Ok(instruments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InstrumentDto>> GetById(int id)
    {
        var instrument = await GetInstrument(id);
        return instrument == null ? NotFound() : Ok(instrument);
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

        var result = await GetInstrument(instrument.Id);

        return CreatedAtAction(nameof(GetById), new { id = instrument.Id }, result);
    }

    private async Task<List<InstrumentDto>> GetInstruments() =>
        await ToDto(BaseQuery()).ToListAsync();

    private async Task<InstrumentDto?> GetInstrument(int id) =>
        await ToDto(BaseQuery().Where(i => i.Id == id)).FirstOrDefaultAsync();

    private IQueryable<InstrumentDto> ToDto(IQueryable<Instrument> query) =>
        query
            .OrderBy(i => i.Id)
            .Select(i => new InstrumentDto(
                i.Id,
                i.Name,
                new InstrumentTypeDto(
                    i.InstrumentType.Id,
                    i.InstrumentType.Name)
            ));

    private IQueryable<Instrument> BaseQuery() => _db.Instruments.AsNoTracking();
}