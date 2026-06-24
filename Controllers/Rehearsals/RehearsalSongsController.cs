using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/rehearsals/{rehearsalId}/songs")]
[Authorize]
public class RehearsalSongsController : ControllerBase
{
    private readonly AppDbContext _db;
    public RehearsalSongsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RehearsalSongDto>>> GetAll(int rehearsalId)
    {
        var rehearsalSongs = await GetRehearsalSongs(rehearsalId);
        return Ok(rehearsalSongs);
    }

    [HttpGet("{songId}")]
    public async Task<ActionResult<RehearsalSongDto>> GetById(int rehearsalId, int songId)
    {
        var rehearsalSong = await GetRehearsalSong(rehearsalId, songId);
        return rehearsalSong == null ? NotFound() : Ok(rehearsalSong);
    }

    [HttpPost]
    public async Task<ActionResult<RehearsalSong>> Create(int rehearsalId, CreateRehearsalSongDto dto)
    {
        var rehearsalSong = new RehearsalSong
        {
            RehearsalId = rehearsalId,
            SongId = dto.SongId,
            RatingId = dto.RatingId
        };

        _db.RehearsalSongs.Add(rehearsalSong);

        await _db.SaveChangesAsync();

        var result = await GetRehearsalSong(rehearsalId, rehearsalSong.SongId);

        return CreatedAtAction(
            nameof(GetById), 
            new 
            { 
                rehearsalId = rehearsalSong.RehearsalId, 
                songId = rehearsalSong.SongId
            }, 
            result
        );
    }

    private async Task<List<RehearsalSongDto>> GetRehearsalSongs(int rehearsalId) =>
        await ToDto(BaseQuery(rehearsalId)).ToListAsync();

    private async Task<RehearsalSongDto?> GetRehearsalSong(int rehearsalId, int songId) =>
        await ToDto(BaseQuery(rehearsalId).Where(rs => rs.SongId == songId)).FirstOrDefaultAsync();

    private IQueryable<RehearsalSongDto> ToDto (IQueryable<RehearsalSong> query) =>
        query
            .OrderBy(rs => rs.RehearsalId)
            .Select(rs => new RehearsalSongDto(
                rs.RehearsalId,
                rs.Rehearsal.Name,
                rs.SongId,
                rs.Song.Name,
                rs.RatingId,
                rs.Rating != null ? rs.Rating.Name : "Not Stated"
            ));

    private IQueryable<RehearsalSong> BaseQuery(int rehearsalId) =>
         _db.RehearsalSongs
            .Where(rs => rs.RehearsalId == rehearsalId)
            .AsNoTracking();
}