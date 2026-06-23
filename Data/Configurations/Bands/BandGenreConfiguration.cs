
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BandGenreConfiguration : IEntityTypeConfiguration<BandGenre>
{
    public void Configure(EntityTypeBuilder<BandGenre> builder)
    {
        builder
            .HasKey(bg => new { bg.BandId, bg.GenreId });

        builder
            .HasOne(bg => bg.Band)
            .WithMany(b => b.BandGenres)
            .HasForeignKey(bg => bg.BandId);

        builder
            .HasOne(bg => bg.Genre)
            .WithMany(g => g.BandGenres)
            .HasForeignKey(bg => bg.GenreId);
    }
}