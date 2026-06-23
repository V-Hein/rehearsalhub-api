public class RehearsalSong
{
    public int RehearsalId { get; set; }
    public Rehearsal Rehearsal { get; set; } = null!;

    public int SongId { get; set; }
    public Song Song { get; set; } = null!;

    public int? RatingId { get; set; }
    public Rating? Rating { get; set; } = null!;
}