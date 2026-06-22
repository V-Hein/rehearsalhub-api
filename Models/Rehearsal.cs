public class Rehearsal
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int BandId { get; set; } 
    public Band Band { get; set; } = null!;

    public int SetlistId { get; set; }
    public Setlist Setlist { get; set; } = null!;

    public int PlaceId { get; set; } 
    public Place Place { get; set; } = null!;

    public DateTime Date { get; set; }
    public string? Note { get; set; }
    public int? TimeSeconds { get; set; } 
    public List<RehearsalMember> RehearsalMembers { get; set; } = new();
}
