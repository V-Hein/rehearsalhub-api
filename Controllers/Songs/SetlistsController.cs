

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/setlists")]
[Authorize]
public class SetlistsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IUserContext _user;
    private int UserId => _user.UserId;

    public SetlistsController(AppDbContext db, IUserContext user)
    {
        _db = db;
        _user = user;
    }    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SetlistDto>>> GetAll()
    {
        var setlists = await GetSetlists();
        return Ok(setlists);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SetlistDto>> GetById(int id)
    {
        var setlist = await GetSetlist(id);
        return setlist == null ? NotFound() : Ok(setlist);
    }

    [HttpPost]
    public async Task<ActionResult<SetlistDto>> Create(CreateSetlistDto dto)
    {
        var setlist = new Setlist
        {
            Name = dto.Name,
            BandId = dto.BandId,
        };

        foreach (var songId in dto.SongIds)
        {
            setlist.SetlistSongs.Add(new SetlistSong
            {
                SongId = songId
            });
        }

        _db.Setlists.Add(setlist);
        await _db.SaveChangesAsync();

        var result = await GetSetlists();

        return CreatedAtAction(nameof(GetById), new { id = setlist.Id }, result);
    }


    private async Task<List<SetlistDto>> GetSetlists() =>
        await ToDto(BaseQuery()).ToListAsync();

    private async Task<SetlistDto?> GetSetlist(int id) =>
        await ToDto(BaseQuery().Where(s => s.Id == id)).FirstOrDefaultAsync();

    private IQueryable<SetlistDto> ToDto(IQueryable<Setlist> query)
        => query
            .OrderBy(s => s.Id)
            .Select(s => new SetlistDto(
                s.Id,
                s.Name,
                s.Band.Name,
                s.SetlistSongs.Count(),
                s.SetlistSongs.Sum(ss => ss.Song.TimeSeconds ?? 0),
                s.SetlistSongs
                    .Select(ss => new SongListItemDto(
                        ss.Song.Id,
                        ss.Song.Name,
                        ss.Song.TimeSeconds
                    )).ToList()
            ));
    private IQueryable<Setlist> BaseQuery() => 
        _db.Setlists
            .Where(s => s.Band.BandMembers.Any(m => m.UserId == UserId))
            .AsNoTracking();
}