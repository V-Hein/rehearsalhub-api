
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class InstrumentConfiguration : IEntityTypeConfiguration<Instrument>
{
    public void Configure(EntityTypeBuilder<Instrument> builder)
    {
        builder
            .HasOne(i => i.InstrumentType)
            .WithMany(t => t.Instruments)
            .HasForeignKey(i => i.InstrumentTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasIndex(i => i.Name)
            .IsUnique();
    }
}