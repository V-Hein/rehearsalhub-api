public class InstrumentTuning
{
    public int InstrumentId { get; set; }
    public Instrument Instrument { get; set; } = null!;

    public int TuningId { get; set; }
    public Tuning Tuning { get; set; } = null!;
}