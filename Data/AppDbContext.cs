

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Tuning> Tunings => Set<Tuning>();
    public DbSet<SongStatus> SongStatuses => Set<SongStatus>();
    public DbSet<SetlistSong> SetlistSongs => Set<SetlistSong>();
    public DbSet<Song> Songs => Set<Song>();
    public DbSet<InstrumentType> InstrumentTypes => Set<InstrumentType>();
    public DbSet<Instrument> Instruments => Set<Instrument>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Band> Bands => Set<Band>();
    public DbSet<BandGenre> BandGenres => Set<BandGenre>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<BandMember> BandMembers => Set<BandMember>();
    public DbSet<Setlist> Setlists => Set<Setlist>();
    public DbSet<Place> Places => Set<Place>();
    public DbSet<RehearsalStatus> RehearsalStatuses => Set<RehearsalStatus>();
    public DbSet<Rehearsal> Rehearsals => Set<Rehearsal>();
    public DbSet<Tab> Tabs => Set<Tab>();
    public DbSet<RehearsalMemberStatus> RehearsalMemberStatuses => Set<RehearsalMemberStatus>();
    public DbSet<RehearsalMember> RehearsalMembers => Set<RehearsalMember>();
    public DbSet<InstrumentTuning> InstrumentTunings => Set<InstrumentTuning>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}