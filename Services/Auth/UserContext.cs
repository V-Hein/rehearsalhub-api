using System.Security.Claims;

public interface IUserContext
{
    int UserId { get; }
    string Email { get; }
    string? Role { get; }
}

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _http;
    public UserContext(IHttpContextAccessor http) => _http = http;

    public int UserId => 
        int.Parse(_http.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    
    public string Email =>
        _http.HttpContext!.User.FindFirstValue(ClaimTypes.Name)!;

    public string Role =>
        _http.HttpContext!.User.FindFirstValue(ClaimTypes.Role)!;
}