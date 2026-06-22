using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/songs/{songId:int}/tabs")]
public class TabsController : ControllerBase
{
    private readonly AppDbContext _db;

    public TabsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tab>>> GetAll()
    {
        var tabs = await GetTabs();
        return Ok(tabs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tab>> GetById(int id)
    {
        var tab = await GetTab(id);

        if (tab == null)
            return NotFound();

        return Ok(tab);
    }

    [HttpPost]
    public async Task<ActionResult<Tab>> Create(int songId, CreateTabDto dto)
    {
        var hasTunings = await _db.InstrumentTunings
            .AnyAsync(it => it.InstrumentId == dto.InstrumentId);

        int? tuningId = null;

        if (hasTunings)
        {
            tuningId = dto.TuningId
                ?? await _db.Songs
                    .Where(s => s.Id == songId)
                    .Select(s => s.TuningId)
                    .FirstOrDefaultAsync();

            if (tuningId == null)
                return BadRequest(new { error = "Tuning is required for this instrument." });

            var validTuning = await _db.InstrumentTunings
                .AnyAsync(it => 
                    it.InstrumentId == dto.InstrumentId && 
                    it.TuningId == tuningId);

            if (!validTuning)
                return BadRequest(new { error = "Invalid tuning for instrument." });
        } 
        
        else if (dto.TuningId.HasValue)
        {
            return BadRequest(new { error = "This instrument does not support tunings." });
        }

        var tab = new Tab
        {
            Name = dto.Name,
            SongId = songId,
            InstrumentId = dto.InstrumentId,
            Url = dto.Url,
            TuningId = tuningId
        };

        _db.Tabs.Add(tab);

        await _db.SaveChangesAsync();

        var result = await GetTab(tab.Id);

        return CreatedAtAction(nameof(GetById), new { songId = tab.SongId, id = tab.Id }, result);
    }

    private async Task<List<TabDto>> GetTabs()
    {
        return await ToDto(BaseQuery()).ToListAsync();
    }

    private async Task<TabDto?> GetTab(int id)
    {
        return await ToDto(BaseQuery().Where(t => t.Id == id)).FirstOrDefaultAsync();
    }

    private IQueryable<TabDto> ToDto(IQueryable<Tab> query)
    {
        return query
            .OrderBy(t => t.Id)
            .Select(t => new TabDto(
                t.Id,
                t.Name,
                t.Song.Name,
                t.Song.Band.Name,
                t.Instrument.Name,
                t.Tuning == null ? null : t.Tuning.Name,
                t.Url
            ));
    }

    private IQueryable<Tab> BaseQuery()
    {
        return _db.Tabs;
    }
}