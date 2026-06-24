

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _db;

    public UsersController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = await GetUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var user = await GetUser(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create(CreateUserDto dto)
    {
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            PasswordHash = dto.PasswordHash
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var result = await GetUser(user.Id);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, result);
    }

    private async Task<List<UserDto>> GetUsers() =>
        await ToDto(BaseQuery()).ToListAsync();

    private async Task<UserDto?> GetUser(int id) =>
        await ToDto(BaseQuery().Where(u => u.Id == id)).FirstOrDefaultAsync();

    private IQueryable<UserDto> ToDto(IQueryable<User> query) =>
        query
            .OrderBy(u => u.Id)
            .Select(u => new UserDto(
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email,
                u.Phone,
                u.Songs
            ));

    private IQueryable<User> BaseQuery() => _db.Users.AsNoTracking();
}