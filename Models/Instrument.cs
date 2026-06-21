public class Instrument
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int InstrumentTypeId { get; set; }
    public InstrumentType InstrumentType { get; set; } = null!;

    public List<Tuning> Tunings { get; set; } = new();
}