

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BandMemberConfiguration : IEntityTypeConfiguration<BandMember>
{
    public void Configure(EntityTypeBuilder<BandMember> builder)
    {
        builder
            .HasKey(bm => new { bm.BandId, bm.UserId });

        builder
            .HasOne(bm => bm.Band)
            .WithMany(b => b.BandMembers)
            .HasForeignKey(bm => bm.BandId);

        builder
            .HasOne(bm => bm.User)
            .WithMany(u => u.BandMembers)
            .HasForeignKey(bm => bm.UserId);

        builder
            .HasOne(bm => bm.Role)
            .WithMany(r => r.BandMembers)
            .HasForeignKey(bm => bm.RoleId);
    }
}