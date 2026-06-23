using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RehearsalStatusConfiguration : IEntityTypeConfiguration<RehearsalStatus>
{
    public void Configure(EntityTypeBuilder<RehearsalStatus> builder)
    {
        builder
            .HasIndex(ss => ss.Name)
            .IsUnique();
    }
}