

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/songs")]
public class SongsController : ControllerBase
{
    private readonly AppDbContext _db;

    public SongsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Song>>> GetAll()
    {
        var songs = await _db.Songs
            .Include(s => s.User)
            .Include(s => s.Tuning)
            .Select(s => new SongDto(
                s.Id,
                s.Name,
                s.Band.Name,
                $"{s.User.FirstName} {s.User.LastName}",
                s.Tuning.Name,
                s.SongStatus.Name,
                s.TimeSeconds,
                s.CoverImage
            )).ToListAsync();

        if (songs == null)
            return NotFound();

        return Ok(songs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Song>> GetById(int id)
    {
        var song = await _db.Songs
            .Where(s => s.Id == id)
            .Include(s => s.User)
            .Include(s => s.Tuning)
            .Select(s => new SongDto(
                s.Id,
                s.Name,
                s.Band.Name,
                $"{s.User.FirstName} {s.User.LastName}",
                s.Tuning.Name,
                s.SongStatus.Name,
                s.TimeSeconds,
                s.CoverImage
            )).FirstOrDefaultAsync();

        if (song == null)
            return NotFound();

        return Ok(song);
    }

    [HttpPost]
    public async Task<ActionResult<Song>> Create(CreateSongDto dto)
    {
        var song = new Song
        {
            Name = dto.Name,
            BandId = dto.BandId,
            UserId = dto.UserId,
            TuningId = dto.TuningId,
            SongStatusId = dto.SongStatusId,
            TimeSeconds = dto.TimeSeconds,
            CoverImage = dto.CoverImagePath
        };

        _db.Songs.Add(song);

        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = song.Id}, song);
    }
}