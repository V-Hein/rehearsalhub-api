using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RehearsalMemberStatusConfiguration : IEntityTypeConfiguration<RehearsalMemberStatus>
{
    public void Configure(EntityTypeBuilder<RehearsalMemberStatus> builder)
    {
        builder
            .HasIndex(ss => ss.Name)
            .IsUnique();
    }
}