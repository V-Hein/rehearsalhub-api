using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class InstrumentTuningConfiguration: IEntityTypeConfiguration<InstrumentTuning>
{
    public void Configure(EntityTypeBuilder<InstrumentTuning> builder)
    {
        builder
            .HasKey(it => new { it.InstrumentId, it.TuningId });
            
        builder
            .HasOne(it => it.Instrument)
            .WithMany(i => i.InstrumentTunings)
            .HasForeignKey(it => it.InstrumentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(it => it.Tuning)
            .WithMany(t => t.InstrumentTunings)
            .HasForeignKey(it => it.TuningId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}