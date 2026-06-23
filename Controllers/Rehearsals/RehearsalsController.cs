

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

[ApiController]
[Route("api/rehearsals")]
public class RehearsalsController : ControllerBase
{
    private readonly AppDbContext _db;

    public RehearsalsController(AppDbContext db)
    {
        _db = db;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Rehearsal>>> GetAll()
    {
        var rehearsals = await GetRehearsals();

        if (rehearsals == null)
            return NotFound();

        return Ok(rehearsals);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Rehearsal>> GetById(int id)
    {
        var rehearsal = await GetRehearsal(id);

        if (rehearsal == null)
            return NotFound();

        return Ok(rehearsal);
    }

    [HttpPost]
    public async Task<ActionResult<Rehearsal>> Create(CreateRehearsalDto dto)
    {
        var rehearsal = new Rehearsal
        {
            Name = dto.Name,
            BandId = dto.BandId,
            SetlistId = dto.SetlistId,
            PlaceId = dto.PlaceId,
            RehearsalStatusId = dto.RehearsalStatusId,
            Date = Convert.ToDateTime(dto.Date),
            TimeSeconds = dto.TimeSeconds,
            Note = dto.Note
        };

        _db.Rehearsals.Add(rehearsal);

        await _db.SaveChangesAsync();

        var result = await GetRehearsal(rehearsal.Id);

        return CreatedAtAction(nameof(GetById), new { id = rehearsal.Id }, result);
    }

    private IQueryable<RehearsalDto> ToDto(IQueryable<Rehearsal> query)
    {
        return query.Select(r => new RehearsalDto(
                r.Id,
                r.Name,
                r.Band.Name,
                r.Setlist.Name,
                r.Place.Name,
                r.RehearsalStatus.Name,
                r.Date,
                TimeSpan.FromSeconds(r.TimeSeconds ?? 0),
                r.Note
            ));
    }

    private IQueryable<Rehearsal> BaseQuery()
    {
        return _db.Rehearsals;
    }

    private async Task<List<RehearsalDto>> GetRehearsals()
    {
        return await ToDto(BaseQuery()).ToListAsync();
    }

    private async Task<RehearsalDto?> GetRehearsal(int id)
    {
        return await ToDto(BaseQuery().Where(r => r.Id == id)).FirstOrDefaultAsync();
    }
}