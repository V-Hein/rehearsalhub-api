
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TuningConfiguration : IEntityTypeConfiguration<Tuning>
{
    public void Configure(EntityTypeBuilder<Tuning> builder)
    {
        builder
            .HasIndex(u => u.Name)
            .IsUnique();   
    }
}