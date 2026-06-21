public class Song
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Band { get; set; } = null!;

    
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int TuningId { get; set; }
    public Tuning Tuning { get; set; } = null!;

    public int? TimeSeconds { get; set; }
    public string? CoverImage { get; set; }
}