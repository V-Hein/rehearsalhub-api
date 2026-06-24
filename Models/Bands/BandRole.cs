public class BandRole
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public List<BandMember> BandMembers { get; set; } = new();
}