public class BandMember
{
    public int BandId { get; set; }
    public Band Band { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
}