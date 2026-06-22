public class Tuning
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Notes { get; set; } = null!;

    public List<Song> Songs { get; set; } = new();
    public List<Tab> Tabs { get; set; } = new();
    public List<InstrumentTuning> InstrumentTunings { get; set; } = new();
}