public class BandMember
{
    public int Id { get; set; }
    public int BandId { get; set; }
    public Band Band { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int BandRoleId { get; set; }
    public BandRole BandRole { get; set; } = null!;

    public List<RehearsalMember> RehearsalMembers { get; set; } = new();
}