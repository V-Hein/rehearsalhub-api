

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/roles")]
[Authorize]
public class BandRolesController : ControllerBase
{
    private readonly AppDbContext _db;
    public BandRolesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BandRoleDto>>> GetAll()
    {
        var roles = await GetRoles();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BandRoleDto>> GetById(int id)
    {
        var role = await GetRole(id);
        return role == null ? NotFound() : Ok(role);
    }

    [HttpPost]
    public async Task<ActionResult<BandRoleDto>> Create(CreateBandRoleDto dto)
    {
        var role = new BandRole { Name = dto.Name };

        _db.BandRoles.Add(role);
        await _db.SaveChangesAsync();

        var result = await GetRole(role.Id);

        return CreatedAtAction(nameof(GetById), new { id = role.Id }, result);
    }

    // Inner Methods

    private async Task<List<BandRoleDto>> GetRoles() =>
        await ToDto(BaseQuery()).ToListAsync();

    private async Task<BandRoleDto?> GetRole(int id) =>
        await ToDto(BaseQuery().Where(r => r.Id == id)).FirstOrDefaultAsync();

    private IQueryable<BandRoleDto> ToDto(IQueryable<BandRole> query) =>
        query
            .OrderBy(r => r.Id)
            .Select(r => new BandRoleDto(
                r.Id,
                r.Name
            ));

    private IQueryable<BandRole> BaseQuery() => _db.BandRoles.AsNoTracking();
}