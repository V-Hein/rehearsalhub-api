using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/song_statuses")]
public class SongStatusesController : ControllerBase
{
    private readonly AppDbContext _db;
    
    public SongStatusesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SongStatus>>> GetAll() =>
        Ok(await GetStatuses());

    [HttpGet("{id}")]
    public async Task<ActionResult<SongStatus>> GetById(int id)
    {
        var status = await GetStatus(id);
        return status == null ? NotFound() : Ok(status);
    }

    [HttpPost]
    public async Task<ActionResult<SongStatusDto>> Create(CreateSongStatusDto dto)
    {
        var status = new SongStatus{ Name = dto.Name };

        _db.SongStatuses.Add(status);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = status.Id}, await GetStatus(status.Id));
    }


    private async Task<List<SongStatusDto>> GetStatuses() =>
        await ToDto(BaseQuery()).ToListAsync();
    private async Task<SongStatusDto?> GetStatus(int id) => 
        await ToDto(BaseQuery().Where(rs => rs.Id == id)).FirstOrDefaultAsync();

    private IQueryable<SongStatusDto> ToDto(IQueryable<SongStatus> query) =>
        query
            .OrderBy(rs => rs.Id)
            .Select(rs => new SongStatusDto(
                rs.Id,
                rs.Name
            ));
    
    private IQueryable<SongStatus> BaseQuery() => _db.SongStatuses.AsNoTracking();
}