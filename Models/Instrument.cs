public class Instrument
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public int InstrumentTypeId { get; set; }
    public InstrumentType InstrumentType { get; set; } = null!;

    public List<Tab> Tabs { get; set; } = new();
    public List<InstrumentTuning>? InstrumentTunings { get; set; }
}