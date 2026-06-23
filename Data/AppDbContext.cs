

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {}

    // Users
    public DbSet<User> Users => Set<User>();

    // Bands
    public DbSet<Band> Bands => Set<Band>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<BandGenre> BandGenres => Set<BandGenre>();
    public DbSet<BandRole> BandRoles => Set<BandRole>();
    public DbSet<BandMember> BandMembers => Set<BandMember>();

    // Instruments
    public DbSet<Instrument> Instruments => Set<Instrument>();
    public DbSet<InstrumentType> InstrumentTypes => Set<InstrumentType>();
    public DbSet<Tuning> Tunings => Set<Tuning>();
    public DbSet<InstrumentTuning> InstrumentTunings => Set<InstrumentTuning>();

    // Songs
    public DbSet<Song> Songs => Set<Song>();
    public DbSet<SongStatus> SongStatuses => Set<SongStatus>();
    public DbSet<Setlist> Setlists => Set<Setlist>();
    public DbSet<SetlistSong> SetlistSongs => Set<SetlistSong>();
    public DbSet<Tab> Tabs => Set<Tab>();

    // Rehearsals
    public DbSet<Rehearsal> Rehearsals => Set<Rehearsal>();
    public DbSet<Place> Places => Set<Place>();
    public DbSet<RehearsalStatus> RehearsalStatuses => Set<RehearsalStatus>();
    public DbSet<RehearsalMember> RehearsalMembers => Set<RehearsalMember>();
    public DbSet<RehearsalMemberStatus> RehearsalMemberStatuses => Set<RehearsalMemberStatus>();
    public DbSet<RehearsalSong> RehearsalSongs => Set<RehearsalSong>();
    public DbSet<Rating> Ratings => Set<Rating>();
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}