

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
            SongStatusId = dto.SongStatusId,
            TimeSeconds = dto.TimeSeconds,
            CoverImage = dto.CoverImagePath
        };

        _db.Songs.Add(song);
        await _db.SaveChangesAsync();

        var result = await GetSong(song.Id);

        return CreatedAtAction(nameof(GetById), new { id = song.Id}, result);
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
                s.Band.Name,
                s.User.FirstName + " " + s.User.LastName,
                s.Tuning.Name,
                s.SongStatus.Name,
                s.TimeSeconds,
                s.CoverImage
            ));

    private IQueryable<Song> BaseQuery() =>
        _db.Songs
            .Where(s => s.Band.BandMembers.Any(m => m.UserId == UserId))
            .AsNoTracking();
}