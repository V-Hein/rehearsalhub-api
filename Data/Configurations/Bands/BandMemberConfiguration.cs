

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BandMemberConfiguration : IEntityTypeConfiguration<BandMember>
{
    public void Configure(EntityTypeBuilder<BandMember> builder)
    {
        builder
            .HasOne(bm => bm.Band)
            .WithMany(b => b.BandMembers)
            .HasForeignKey(bm => bm.BandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(bm => bm.User)
            .WithMany(u => u.BandMembers)
            .HasForeignKey(bm => bm.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(bm => bm.BandRole)
            .WithMany(r => r.BandMembers)
            .HasForeignKey(bm => bm.BandRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasIndex(bm => new { bm.BandId, bm.UserId })
            .IsUnique();
    }
}