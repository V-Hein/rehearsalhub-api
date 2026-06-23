

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly AppDbContext _db;

    public RolesController(AppDbContext db)
    {
        _db = db;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<BandRole>>> GetAll()
    {
        var roles = await _db.BandRoles
            .Select(r => new BandRoleDto(
                r.Id,
                r.Name
            )).ToListAsync();

        if (roles == null)
            return NotFound();

        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BandRole>> GetById(int id)
    {
        var role = await _db.BandRoles
            .Where(r => r.Id == id)
            .Select(r => new BandRoleDto(
                r.Id,
                r.Name
            )).FirstOrDefaultAsync();

        if (role == null)
            return NotFound();

        return Ok(role);
    }

    [HttpPost]
    public async Task<ActionResult<BandRole>> Create(CreateBandRoleDto dto)
    {
        var role = new BandRole
        {
            Name = dto.Name
        };

        _db.BandRoles.Add(role);

        await _db.SaveChangesAsync();

        var result = await _db.BandRoles
            .Where(r => r.Id == role.Id)
            .Select(r => new BandRoleDto(
                r.Id,
                r.Name
            )).FirstOrDefaultAsync();

        return CreatedAtAction(nameof(GetById), new { id = role.Id }, result);
    }
}