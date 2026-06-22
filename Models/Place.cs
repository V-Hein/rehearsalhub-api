public class Place
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;

    public List<Rehearsal> Rehearsals { get; set; } = new();
}