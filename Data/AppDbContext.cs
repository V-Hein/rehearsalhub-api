

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Tuning> Tunings => Set<Tuning>();
    public DbSet<Song> Songs => Set<Song>();
    public DbSet<InstrumentType> InstrumentTypes => Set<InstrumentType>();
    public DbSet<Instrument> Instruments => Set<Instrument>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Band> Bands => Set<Band>();
    public DbSet<BandGenre> BandGenres => Set<BandGenre>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<BandMember> BandMembers => Set<BandMember>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}