using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RehearsalSongConfiguration : IEntityTypeConfiguration<RehearsalSong>
{
    public void Configure(EntityTypeBuilder<RehearsalSong> builder)
    {
        builder
            .HasKey(rs => new { rs.RehearsalId, rs.SongId });
            
        builder
            .HasOne(rs => rs.Rehearsal)
            .WithMany(r => r.RehearsalSongs)
            .HasForeignKey(rs => rs.RehearsalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(rs => rs.Song)
            .WithMany(s => s.RehearsalSongs)
            .HasForeignKey(rs => rs.SongId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(rs => rs.Rating)
            .WithMany(r => r.RehearsalSongs)
            .HasForeignKey(rs => rs.RatingId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}