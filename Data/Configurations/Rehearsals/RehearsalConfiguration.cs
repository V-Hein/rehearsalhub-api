
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RehearsalConfiguration : IEntityTypeConfiguration<Rehearsal>
{
    public void Configure(EntityTypeBuilder<Rehearsal> builder)
    {
        builder
            .HasOne(r => r.Band)
            .WithMany(b => b.Rehearsals)
            .HasForeignKey(r => r.BandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(r => r.Setlist)
            .WithMany(s => s.Rehearsals)
            .HasForeignKey(r => r.SetlistId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(r => r.Place)
            .WithMany(p => p.Rehearsals)
            .HasForeignKey(r => r.PlaceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(r => r.RehearsalStatus)
            .WithMany(rs => rs.Rehearsals)
            .HasForeignKey(r => r.RehearsalStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasIndex(r => r.Name)
            .IsUnique();
    }
}