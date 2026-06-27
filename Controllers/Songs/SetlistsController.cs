

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

    [HttpPut("{id}")]
    public async Task<ActionResult<SetlistDto>> Update(int id, CreateSetlistDto dto)
    {
        var setlist = await _db.Setlists
            .Include(s => s.SetlistSongs)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (setlist == null)
            return NotFound();

        setlist.Name = dto.Name;
        setlist.BandId = dto.BandId;
        setlist.SetlistSongs.Clear();
        
        foreach (var songId in dto.SongIds)
            setlist.SetlistSongs.Add(new SetlistSong
            {
                SetlistId = setlist.Id,
                SongId = songId
            });

        await _db.SaveChangesAsync();

        return Ok(await GetSetlist(id));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (usedInRehearsal(id))
            return Problem(
                detail: "Setlist cannot be deleted because it currently used in Rehearsal",
                statusCode: StatusCodes.Status409Conflict,
                title: "Resource Conflict"
            );

        var setlist = await _db.Setlists
            .Include(s => s.SetlistSongs)
            .FirstOrDefaultAsync(s => s.Id == id);
        
        if (setlist == null) 
            return NotFound();

        _db.SetlistSongs.RemoveRange(setlist.SetlistSongs);
        _db.Setlists.Remove(setlist);

        await _db.SaveChangesAsync();

        return NoContent();
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
                s.Band.Id,
                s.Band.Name,
                s.SetlistSongs
                    .Select(ss => ss.Song.Id)
                    .ToList(),
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
            // .Where(s => s.Band.BandMembers.Any(m => m.UserId == UserId))
            .AsNoTracking();

    private bool usedInRehearsal(int id) =>
        _db.Rehearsals.Any(r => r.SetlistId == id);
}