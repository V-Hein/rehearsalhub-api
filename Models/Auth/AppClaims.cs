using System.Security.Claims;

public static class AppClaims
{
    public const string UserId = ClaimTypes.NameIdentifier;
    public const string Email = ClaimTypes.Name;
    public const string Role = ClaimTypes.Role;
}