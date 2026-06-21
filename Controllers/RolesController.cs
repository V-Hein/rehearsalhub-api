

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
    public async Task<ActionResult<IEnumerable<Role>>> GetAll()
    {
        var roles = await _db.Roles
            .Select(r => new RoleDto(
                r.Id,
                r.Name
            )).ToListAsync();

        if (roles == null)
            return NotFound();

        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> GetById(int id)
    {
        var role = await _db.Roles
            .Where(r => r.Id == id)
            .Select(r => new RoleDto(
                r.Id,
                r.Name
            )).FirstOrDefaultAsync();

        if (role == null)
            return NotFound();

        return Ok(role);
    }

    [HttpPost]
    public async Task<ActionResult<Role>> Create(CreateRoleDto dto)
    {
        var role = new Role
        {
            Name = dto.Name
        };

        _db.Roles.Add(role);

        await _db.SaveChangesAsync();

        var result = await _db.Roles
            .Where(r => r.Id == role.Id)
            .Select(r => new RoleDto(
                r.Id,
                r.Name
            )).FirstOrDefaultAsync();

        return CreatedAtAction(nameof(GetById), new { id = role.Id }, result);
    }
}