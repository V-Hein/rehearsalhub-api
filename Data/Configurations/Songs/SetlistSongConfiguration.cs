using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SetlistSongConfiguration : IEntityTypeConfiguration<SetlistSong>
{
    public void Configure(EntityTypeBuilder<SetlistSong> builder)
    {
        builder
            .HasKey(ss => new { ss.SetlistId, ss.SongId });

        builder
            .HasOne(ss => ss.Setlist)
            .WithMany(s => s.SetlistSongs)
            .HasForeignKey(ss => ss.SetlistId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(ss => ss.Song)
            .WithMany(s => s.SetlistSongs)
            .HasForeignKey(ss => ss.SongId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}