public class Song
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int BandId { get; set; }
    public Band Band { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int TuningId { get; set; }
    public Tuning Tuning { get; set; } = null!;

    public int SongStatusId { get; set; }
    public SongStatus SongStatus { get; set; } = null!;

    public int? TimeSeconds { get; set; }
    public string? CoverImage { get; set; }
    
    public List<Tab> Tabs { get; set; } = new();
    public List<SetlistSong> SetlistSongs { get; set; } = new();
}