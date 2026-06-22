using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RehearsalMemberConfiguration : IEntityTypeConfiguration<RehearsalMember>
{
    public void Configure(EntityTypeBuilder<RehearsalMember> builder)
    {
        builder
            .HasKey(rm => new { rm.RehearsalId, rm.BandMemberId });


        builder
            .HasOne(rm => rm.Rehearsal)
            .WithMany(r => r.RehearsalMembers)
            .HasForeignKey(rm => rm.RehearsalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(rm => rm.BandMember)
            .WithMany(u => u.RehearsalMembers)
            .HasForeignKey(rm => rm.BandMemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(rm => rm.RehearsalMemberStatus)
            .WithMany(rms => rms.RehearsalMembers)
            .HasForeignKey(rm => rm.RehearsalMemberStatusId)
            .OnDelete(DeleteBehavior.Restrict);      
    }
}