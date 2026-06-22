public class Tab
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;

    public int SongId { get; set; } 
    public Song Song { get; set; } = null!;

    public int InstrumentId { get; set; }
    public Instrument Instrument { get; set; } = null!;

    public int? TuningId { get; set; }
    public Tuning? Tuning { get; set;}
}