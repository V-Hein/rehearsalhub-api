public class SetlistSong
{
    public int SetlistId { get; set; }
    public Setlist Setlist { get; set; } = null!;

    public int SongId { get; set; }
    public Song Song { get; set; } = null!;
}