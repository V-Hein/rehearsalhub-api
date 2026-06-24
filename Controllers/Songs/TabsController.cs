using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/songs/{songId:int}/tabs")]
[Authorize]
public class TabsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IUserContext _user;
    private int UserId => _user.UserId;

    public TabsController(AppDbContext db, IUserContext user)
    {
        _db = db;
        _user = user;
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
        return tab == null ? NotFound() : Ok(tab);
    }

    [HttpPost]
    public async Task<ActionResult<Tab>> Create(int songId, CreateTabDto dto)
    {
        var tab = new Tab
        {
            Name = dto.Name,
            SongId = songId,
            InstrumentId = dto.InstrumentId,
            Url = dto.Url,
            TuningId = dto.TuningId
        };

        _db.Tabs.Add(tab);
        await _db.SaveChangesAsync();

        var result = await GetTab(tab.Id);

        return CreatedAtAction(nameof(GetById), new { songId = tab.SongId, id = tab.Id }, result);
    }

    private async Task<List<TabDto>> GetTabs() =>
        await ToDto(BaseQuery()).ToListAsync();

    private async Task<TabDto?> GetTab(int id) =>
        await ToDto(BaseQuery().Where(t => t.Id == id)).FirstOrDefaultAsync();

    private IQueryable<TabDto> ToDto(IQueryable<Tab> query) =>
        query
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

    private IQueryable<Tab> BaseQuery() => 
        _db.Tabs
            .Where(t => t.Song.Band.BandMembers.Any(m => m.UserId == UserId))
            .AsNoTracking();
}