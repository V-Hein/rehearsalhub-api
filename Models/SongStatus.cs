public class SongStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public List<Song> Songs { get; set; } = new();
}