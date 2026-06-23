public class RehearsalStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public List<Rehearsal> Rehearsals { get; set; } = new();
}