
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TuningConfiguration : IEntityTypeConfiguration<Tuning>
{
    public void Configure(EntityTypeBuilder<Tuning> builder)
    {
        builder
            .HasOne(t => t.Instrument)
            .WithMany(i => i.Tunings)
            .HasForeignKey(t => t.InstrumentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(u => u.Name)
            .IsUnique();   
    }
}