public class Band
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public List<BandGenre> BandGenres { get; set; } = new();
    public List<BandMember> BandMembers { get; set; } = new();
}