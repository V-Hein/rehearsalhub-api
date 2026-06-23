public class Setlist
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int BandId { get; set; }
    public Band Band { get; set; } = null!;

    public List<Rehearsal> Rehearsals { get; set; } = new();
    public List<SetlistSong> SetlistSongs { get; set; } = new();
}