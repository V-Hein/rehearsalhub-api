public class InstrumentType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public List<Instrument> Instruments { get; set; } = new();
}