using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/rehearsals/member_statuses")]
[Authorize]
public class RehearsalMemberStatusesController : ControllerBase
{
    private readonly AppDbContext _db;
    public RehearsalMemberStatusesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RehearsalMemberStatusDto>>> GetAll()
    {
        var statuses = await GetStatuses();
        return Ok(statuses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RehearsalMemberStatusDto>> GetById(int id)
    {
        var status = await GetStatus(id);
        return status == null ? NotFound() : Ok(status);
    }

    [HttpPost]
    public async Task<ActionResult<RehearsalMemberStatusDto>> Create(CreateRehearsalMebmerStatusDto dto)
    {
        var status = new RehearsalMemberStatus{ Name = dto.Name };

        _db.RehearsalMemberStatuses.Add(status);
        await _db.SaveChangesAsync();

        var result = await GetStatus(status.Id);

        return CreatedAtAction(nameof(GetById), new { id = status.Id }, result );
    }

    private async Task<List<RehearsalMemberStatusDto>> GetStatuses() =>
        await ToDto(BaseQuery()).ToListAsync();

    private async Task<RehearsalMemberStatusDto?> GetStatus(int id) =>
        await ToDto(BaseQuery().Where(rms => rms.Id == id)).FirstOrDefaultAsync();
    
    private IQueryable<RehearsalMemberStatusDto> ToDto(IQueryable<RehearsalMemberStatus> query)
        => query
            .OrderBy(rms => rms.Id)
            .Select(rms => new RehearsalMemberStatusDto(
                rms.Id,
                rms.Name
            ));

    private IQueryable<RehearsalMemberStatus> BaseQuery() =>
        _db.RehearsalMemberStatuses.AsNoTracking();
}