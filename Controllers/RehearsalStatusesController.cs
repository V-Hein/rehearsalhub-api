using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/rehearsal_statuses")]
public class RehearsalStatusesController : ControllerBase
{
    private readonly AppDbContext _db;
    
    public RehearsalStatusesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RehearsalStatusDto>>> GetAll() =>
        Ok(await GetStatuses());

    [HttpGet("{id}")]
    public async Task<ActionResult<RehearsalStatusDto>> GetById(int id)
    {
        var status = await GetStatus(id);
        return status == null ? NotFound() : Ok(status);
    }

    [HttpPost]
    public async Task<ActionResult<RehearsalStatusDto>> Create(CreateRehearsalStatusDto dto)
    {
        var status = new RehearsalStatus{ Name = dto.Name };

        _db.RehearsalStatuses.Add(status);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = status.Id}, await GetStatus(status.Id));
    }



    private async Task<List<RehearsalStatusDto>> GetStatuses() =>
        await ToDto(BaseQuery()).ToListAsync();
    private async Task<RehearsalStatusDto?> GetStatus(int id) => 
        await ToDto(BaseQuery().Where(rs => rs.Id == id)).FirstOrDefaultAsync();

    private IQueryable<RehearsalStatusDto> ToDto(IQueryable<RehearsalStatus> query) =>
        query
            .OrderBy(rs => rs.Id)
            .Select(rs => new RehearsalStatusDto(
                rs.Id,
                rs.Name
            ));
    
    private IQueryable<RehearsalStatus> BaseQuery() => _db.RehearsalStatuses.AsNoTracking();
}