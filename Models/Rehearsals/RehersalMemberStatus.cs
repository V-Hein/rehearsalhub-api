public class RehearsalMemberStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public List<RehearsalMember> RehearsalMembers { get; set; } = new();
}