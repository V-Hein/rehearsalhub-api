

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/songs")]
[Authorize]
public class SongsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IUserContext _user;
    private int UserId => _user.UserId;

    public SongsController(AppDbContext db, IUserContext user)
    {
        _db = db;
        _user = user;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SongDto>>> GetAll()
    {
        var songs = await GetSongs();
        return Ok(songs);
    }
        
    [HttpGet("{id}")]
    public async Task<ActionResult<SongDto>> GetById(int id)
    {
        var song = await GetSong(id);
        return song == null ? NotFound() : Ok(song);
    }

    [HttpPost]
    public async Task<ActionResult<SongDto>> Create(CreateSongDto dto)
    {
        var song = new Song
        {
            Name = dto.Name,
            BandId = dto.BandId,
            UserId = UserId,
            TuningId = dto.TuningId,
            SongStatusId = dto.StatusId,
            TimeSeconds = dto.TimeSeconds,
            CoverImage = dto.CoverImagePath
        };

        _db.Songs.Add(song);
        await _db.SaveChangesAsync();

        var result = await GetSong(song.Id);

        return CreatedAtAction(nameof(GetById), new { id = song.Id}, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<SongDto>> Update(int id, CreateSongDto dto)
    {
        var song = await _db.Songs.FindAsync(id);

        if (song == null)
            return NotFound();

        song.Name = dto.Name;
        song.BandId = dto.BandId;
        song.TuningId = dto.TuningId;
        song.SongStatusId = dto.StatusId;
        song.TimeSeconds = dto.TimeSeconds;
        song.CoverImage = dto.CoverImagePath;

        await _db.SaveChangesAsync();
        
        return Ok(await GetSong(id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (IsInSetlist(id))
            return Problem(
                detail: "The song cannot be deleted because it is currently used in a setlist.",
                statusCode: StatusCodes.Status409Conflict,
                title: "Resource Conflict"
            );
            
        var song = await _db.Songs.FindAsync(id);
        
        if (song == null) 
            return NotFound();
        
        _db.Songs.Remove(song);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    private async Task<List<SongDto>> GetSongs() =>
        await ToDto(BaseQuery()).ToListAsync();

    private async Task<SongDto?> GetSong(int id) =>
        await ToDto(BaseQuery().Where(s => s.Id == id)).FirstOrDefaultAsync();

    private IQueryable<SongDto> ToDto(IQueryable<Song> query) =>
        query
            .OrderBy(s => s.Id)
            .Select(s => new SongDto(
                s.Id,
                s.Name,
                s.BandId,
                s.Band.Name,
                s.User.FirstName + " " + s.User.LastName,
                s.TuningId,
                s.Tuning.Name,
                s.SongStatusId,
                s.SongStatus.Name,
                s.TimeSeconds,
                s.CoverImage
            ));

    private IQueryable<Song> BaseQuery() =>
        _db.Songs
            // .Where(s => s.Band.BandMembers.Any(m => m.UserId == UserId))
            .AsNoTracking();

    private bool IsInSetlist(int id) =>
        _db.SetlistSongs.Any(ss => ss.SongId == id);
}