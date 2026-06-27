using Microsoft.EntityFrameworkCore;

public interface IAuthService
{
    Task<AuthResponseDto?> Login(LoginDto dto);
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly IJwtTokenGenerator _jwt;

    public AuthService(AppDbContext db, IJwtTokenGenerator jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    public async Task<AuthResponseDto?> Login(LoginDto dto)
    {
        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == dto.Email);
        
        if (user == null)
            return null;

        if (user.PasswordHash != dto.Password)
            return null;

        var token = _jwt.Generate(user);

        return new AuthResponseDto(token, user.Email);
    }

}