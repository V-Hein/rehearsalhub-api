public class RehearsalMember
{
    public int RehearsalId { get; set; }
    public Rehearsal Rehearsal { get; set; } = null!;

    public int BandMemberId { get; set; }
    public BandMember BandMember { get; set; } = null!;

    public int RehearsalMemberStatusId { get; set; }
    public RehearsalMemberStatus RehearsalMemberStatus { get; set; } = null!;
}