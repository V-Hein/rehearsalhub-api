public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }

    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;

    public List<Song> Songs { get; set; } = new();
    public List<BandMember> BandMembers { get; set; } = new();
    public List<RehearsalMember> RehearsalMembers { get; set; } = new();
}