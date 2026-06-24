using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/instruments/{instrumentId}/tunings")]
[Authorize]
public class InstrumentTuningsController : ControllerBase
{
    private readonly AppDbContext _db;
    public InstrumentTuningsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InstrumentTuningDto>>> GetAll(int instrumentId)
    {
        var instrumentTunings = await GetInstrumentTunings(instrumentId);
        return Ok(instrumentTunings);
    }

    [HttpGet("{tuningId}")]
    public async Task<ActionResult<InstrumentTuningDto>> GetById(int instrumentId, int tuningId)
    {
        var instrumentTuning = await GetInstrumentTuning(instrumentId, tuningId);
        return instrumentTuning == null ? NotFound() : Ok(instrumentTuning);
    }

    [HttpPost]
    public async Task<ActionResult<InstrumentTuningDto>> Create(int instrumentId, CreateInstrumentTuningDto dto)
    {
        var instrumentTuning = new InstrumentTuning
        {
            InstrumentId = instrumentId,
            TuningId = dto.TuningId
        };

        _db.InstrumentTunings.Add(instrumentTuning);
        await _db.SaveChangesAsync();

        var result = await GetInstrumentTuning(instrumentId, dto.TuningId);

        return CreatedAtAction(
            nameof(GetById), 
            new 
            { 
                instrumentId = instrumentTuning.InstrumentId, 
                tuningId = instrumentTuning.TuningId 
            }, 
            result
        );
    }

    private async Task<List<InstrumentTuningDto>> GetInstrumentTunings(int instrumentId) =>
        await ToDto(BaseQuery(instrumentId)).ToListAsync();

    private async Task<InstrumentTuningDto?> GetInstrumentTuning(int instrumentId, int tuningId) =>
        await ToDto(BaseQuery(instrumentId)
            .Where(it => it.TuningId == tuningId))
            .FirstOrDefaultAsync();
    
    private IQueryable<InstrumentTuningDto> ToDto(IQueryable<InstrumentTuning> query) =>
        query
            .OrderBy(it => it.InstrumentId)
            .Select(it => new InstrumentTuningDto(
                it.InstrumentId,
                it.Instrument.Name,
                it.TuningId,
                it.Tuning.Name
            ));

    private IQueryable<InstrumentTuning> BaseQuery(int instrumentId) => 
        _db.InstrumentTunings
            .Where(it => it.InstrumentId == instrumentId)
            .AsNoTracking();
}