
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SetlistConfigurations : IEntityTypeConfiguration<Setlist>
{
    public void Configure(EntityTypeBuilder<Setlist> builder)
    {
        builder
            .HasOne(s => s.Band)
            .WithMany(b => b.Setlists)
            .HasForeignKey(s => s.BandId);

        builder
            .HasIndex(s => s.Name)
            .IsUnique();
    }
}