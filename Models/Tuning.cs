public class Tuning
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string Notes { get; set; } = null!;

    public int InstrumentId { get; set; }
    public Instrument Instrument { get; set; } = null!;

    public List<Song> Songs { get; set; } = new();
}