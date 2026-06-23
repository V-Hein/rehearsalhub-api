public class Band
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public List<Song> Songs { get; set; } = new();
    public List<BandGenre> BandGenres { get; set; } = new();
    public List<BandMember> BandMembers { get; set; } = new();
    public List<Setlist> Setlists { get; set; } = new();
    public List<Rehearsal> Rehearsals { get; set; } = new();
    public List<RehearsalMember> RehearsalMembers { get; set; } = new();
}