using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SongStatusConfiguration : IEntityTypeConfiguration<SongStatus>
{
    public void Configure(EntityTypeBuilder<SongStatus> builder)
    {
        builder
            .HasIndex(ss => ss.Name)
            .IsUnique();
    }
}