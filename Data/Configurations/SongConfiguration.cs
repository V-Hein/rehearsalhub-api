
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SongConfiguration : IEntityTypeConfiguration<Song>
{
    public void Configure(EntityTypeBuilder<Song> builder)
    {
        builder
            .HasOne(s => s.User)
            .WithMany(u => u.Songs)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(s => s.Tuning)
            .WithMany(t => t.Songs)
            .HasForeignKey(s => s.TuningId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}