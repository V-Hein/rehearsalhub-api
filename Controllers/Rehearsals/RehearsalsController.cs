

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Server.Migrations;

[ApiController]
[Route("api/rehearsals")]
[Authorize]
public class RehearsalsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IUserContext _user;
    private int UserId => _user.UserId;

    public RehearsalsController(AppDbContext db, IUserContext user)
    {
        _db = db;
        _user = user;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RehearsalDto>>> GetAll()
    {
        var rehearsals = await GetRehearsals();
        return Ok(rehearsals);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RehearsalDto>> GetById(int id)
    {
        var rehearsal = await GetRehearsal(id);
        return rehearsal == null ? NotFound() : Ok(rehearsal);
    }

    [HttpPost]
    public async Task<ActionResult<RehearsalDto>> Create(CreateRehearsalDto dto)
    {
        var rehearsal = new Rehearsal
        {
            Name = dto.Name,
            BandId = dto.BandId,
            SetlistId = dto.SetlistId,
            PlaceId = dto.PlaceId,
            RehearsalStatusId = dto.StatusId,
            Date = Convert.ToDateTime(dto.Date),
            TimeSeconds = dto.TimeSeconds,
            Note = dto.Note
        };

        var songIds = await _db.SetlistSongs
            .Where(ss => ss.SetlistId == rehearsal.SetlistId)
            .Select(ss => ss.SongId)
            .ToListAsync();
        
        foreach (var songId in songIds)
            rehearsal.RehearsalSongs.Add(new RehearsalSong
            {
                RehearsalId = rehearsal.Id,
                SongId = songId
            });

        _db.Rehearsals.Add(rehearsal);
        await _db.SaveChangesAsync();

        var result = await GetRehearsal(rehearsal.Id);

        return CreatedAtAction(nameof(GetById), new { id = rehearsal.Id }, result);
    }

    private async Task<List<RehearsalDto>> GetRehearsals() =>
        await ToDto(BaseQuery()).ToListAsync();

    private async Task<RehearsalDto?> GetRehearsal(int id) =>
        await ToDto(BaseQuery().Where(r => r.Id == id)).FirstOrDefaultAsync();

    private IQueryable<RehearsalDto> ToDto(IQueryable<Rehearsal> query) =>
        query
            .OrderBy(r => r.Id)
            .Select(r => new RehearsalDto(
                r.Id,
                r.Name,
                r.Band.Name,
                r.Setlist.Name,
                r.Place.Name,
                r.RehearsalStatus.Name,
                r.Date,
                r.TimeSeconds ?? 0,
                r.Note,
                r.RehearsalSongs
                    .OrderBy(rs => rs.SongId)
                    .Select(rs => new RehearsalSongListItemDto(
                        rs.Song.Id,
                        rs.Song.Name,
                        rs.Song.TimeSeconds,
                        rs.Rating != null ? rs.Rating.Name : "Not Stated"
                    )).ToList()
            ));

    private IQueryable<Rehearsal> BaseQuery() =>
        _db.Rehearsals
            .Where(r => r.Band.BandMembers.Any(m => m.UserId == UserId))
            .AsNoTracking();
}