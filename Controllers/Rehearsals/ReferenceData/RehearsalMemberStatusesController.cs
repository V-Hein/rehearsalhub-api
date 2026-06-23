using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/rehearsals/member_statuses")]
public class RehearsalMemberStatusesController : ControllerBase
{
    private readonly AppDbContext _db;

    public RehearsalMemberStatusesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RehearsalMemberStatus>>> GetAll()
    {
        var statuses = await GetStatuses();

        return Ok(statuses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RehearsalMemberStatus>> GetById(int id)
    {
        var status = await GetStatus(id);

        if (status == null)
            return NotFound();

        return Ok(status);
    }

    [HttpPost]
    public async Task<ActionResult<RehearsalMemberStatus>> Create(CreateRehearsalMebmerStatusDto dto)
    {
        var status = new RehearsalMemberStatus
        {
            Name = dto.Name
        };

        _db.RehearsalMemberStatuses.Add(status);

        await _db.SaveChangesAsync();

        var result = await GetStatus(status.Id);

        return CreatedAtAction(nameof(GetById), new { id = status.Id }, result );
    }


    private async Task<List<RehearsalMemberStatusDto>> GetStatuses()
    {
        return await ToDto(BaseQuery()).ToListAsync();
    }

    private async Task<RehearsalMemberStatusDto?> GetStatus(int id)
    {
        return await ToDto(BaseQuery().Where(rms => rms.Id == id)).FirstOrDefaultAsync();
    }

 
    private IQueryable<RehearsalMemberStatusDto> ToDto(IQueryable<RehearsalMemberStatus> query)
    {
        return query
            .OrderBy(rms => rms.Id)
            .Select(rms => new RehearsalMemberStatusDto(
            rms.Id,
            rms.Name
        ));
    }

    private IQueryable<RehearsalMemberStatus> BaseQuery()
    {
        return _db.RehearsalMemberStatuses;
    }
}