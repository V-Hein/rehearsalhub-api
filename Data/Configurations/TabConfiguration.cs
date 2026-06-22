
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TabConfiguration : IEntityTypeConfiguration<Tab>
{
    public void Configure(EntityTypeBuilder<Tab> builder)
    {
        builder
            .HasIndex(t => t.Name)
            .IsUnique();

        builder
            .HasOne(t => t.Instrument)
            .WithMany(i => i.Tabs)
            .HasForeignKey(t => t.InstrumentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(t => t.Song)
            .WithMany(s => s.Tabs)
            .HasForeignKey(t => t.SongId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(t => t.Tuning)
            .WithMany(tu => tu.Tabs)
            .HasForeignKey(t => t.TuningId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}