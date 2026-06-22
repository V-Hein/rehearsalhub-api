

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/setlists")]
public class SetlistsController : ControllerBase
{
    private readonly AppDbContext _db;
    
    public SetlistsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Setlist>>> GetAll()
    {
        var setlists = await _db.Setlists
            .Include(s => s.Band)
            .Select(s => new SetlistDto(
                s.Id,
                s.Name,
                s.Band.Name,
                s.SongQuantity,
                s.TimeSeconds.HasValue
                    ? TimeSpan.FromSeconds(s.TimeSeconds.Value)
                    : TimeSpan.Zero
            )).ToListAsync();
        
        if (setlists == null)
            return NotFound();

        return Ok(setlists);
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<Setlist>> GetById(int id)
    {
        var setlist = await _db.Setlists
            .Include(s => s.Band)
            .Where(s => s.Id == id)
            .Select(s => new SetlistDto(
                s.Id,
                s.Name,
                s.Band.Name,
                s.SongQuantity,
                s.TimeSeconds.HasValue
                    ? TimeSpan.FromSeconds(s.TimeSeconds.Value)
                    : TimeSpan.Zero
            )).FirstOrDefaultAsync();
        
        if (setlist == null)
            return NotFound();

        return Ok(setlist);
    }

    [HttpPost]
    public async Task<ActionResult<Setlist>> Create(CreateSetlistDto dto)
    {
        var setlist = new Setlist
        {
            Name = dto.Name,
            BandId = dto.BandId,
            SongQuantity = dto.SongQuantity,
            TimeSeconds = dto.TimeSeconds
        };

        _db.Setlists.Add(setlist);

        await _db.SaveChangesAsync();

        var result = await _db.Setlists
            .Include(s => s.Band)
            .Where(s => s.Id == setlist.Id)
            .Select(s => new SetlistDto(
                s.Id,
                s.Name,
                s.Band.Name,
                s.SongQuantity,
                TimeSpan.FromSeconds(s.TimeSeconds ?? 0)
            )).FirstOrDefaultAsync();

        return CreatedAtAction(nameof(GetById), new { id = setlist.Id }, result);
    }

}