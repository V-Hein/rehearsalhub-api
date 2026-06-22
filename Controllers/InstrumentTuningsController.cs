using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/instruments/{instrumentId}/tunings")]
public class InstrumentTuningsController : ControllerBase
{
    private readonly AppDbContext _db;

    public InstrumentTuningsController(AppDbContext db)
    {
        _db = db;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<InstrumentTuning>>> GetAll(int instrumentId)
    {
        return Ok(await GetInstrumentTunings(instrumentId));
    }

    [HttpGet("{tuningId}")]
    public async Task<ActionResult<InstrumentTuning>> GetById(int instrumentId, int tuningId)
    {
        var instrumentTuning = await GetInstrumentTuning(instrumentId, tuningId);

        if (instrumentTuning == null)
            return NotFound();

        return Ok(instrumentTuning);
    }

    [HttpPost]
    public async Task<ActionResult<InstrumentTuning>> Create(int instrumentId, CreateInstrumentTuningDto dto)
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
    


    private async Task<List<InstrumentTuningDto>> GetInstrumentTunings(int instrumentId)
    {
        return await ToDto(BaseQuery()
            .Where(it => it.InstrumentId == instrumentId))
            .ToListAsync();
    }

    private async Task<InstrumentTuningDto?> GetInstrumentTuning(int instrumentId, int tuningId)
    {
        return await ToDto(BaseQuery()
            .Where(it => 
                it.InstrumentId == instrumentId && 
                it.TuningId == tuningId))
            .FirstOrDefaultAsync();
    }
    
    private IQueryable<InstrumentTuningDto> ToDto(IQueryable<InstrumentTuning> query)
    {
        return query
            .OrderBy(it => it.InstrumentId)
            .Select(it => new InstrumentTuningDto(
                it.InstrumentId,
                it.Instrument.Name,
                it.TuningId,
                it.Tuning.Name
            ));
    }

    private IQueryable<InstrumentTuning> BaseQuery()
    {
        return _db.InstrumentTunings.AsNoTracking();
    }
}