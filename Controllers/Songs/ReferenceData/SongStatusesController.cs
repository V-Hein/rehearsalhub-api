using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.CompilerServices;
using Server.Migrations;
[ApiController]
[Route("api/song_statuses")]
[Authorize]
public class SongStatusesController : ControllerBase
{
    private readonly AppDbContext _db;
    public SongStatusesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SongStatusDto>>> GetAll()
    {
        var songStatuses = await GetStatuses();
        return Ok(songStatuses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SongStatusDto>> GetById(int id)
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

        var result = await GetStatus(status.Id);

        return CreatedAtAction(nameof(GetById), new { id = status.Id}, result);
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