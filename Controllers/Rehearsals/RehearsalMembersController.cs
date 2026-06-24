using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
[ApiController]
[Route("api/rehearsals/{rehearsalId:int}/members")]
[Authorize]
public class RehearsalMembersController : ControllerBase
{
    private readonly AppDbContext _db;
    public RehearsalMembersController(AppDbContext db) => _db = db;

    // Http-Endpoints

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RehearsalMember>>> GetAll(int rehearsalId)
    {
        var members = await GetMembers(rehearsalId);
        return Ok(members);
    }
    
    [HttpGet("{bandMemberId}")]
    public async Task<ActionResult<RehearsalMember>> GetById(int rehearsalId, int bandMemberId)
    {
        var member = await GetMember(rehearsalId, bandMemberId);
        return member == null ? NotFound() :  Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult<RehearsalMember>> Create(int rehearsalId, CreateRehearsalMemberDto dto)
    {
        var member = new RehearsalMember
        {
            RehearsalId = rehearsalId,
            BandMemberId = dto.BandMemberId,
            RehearsalMemberStatusId = dto.StatusId,
        };

        _db.RehearsalMembers.Add(member);

        await _db.SaveChangesAsync();

        var result = await GetMember(member.RehearsalId, member.BandMemberId);

        return CreatedAtAction(nameof(GetById), new { rehearsalId = member.RehearsalId, bandMemberId = member.BandMemberId }, result);
    }

    // Inner-Methods

    private async Task<List<RehearsalMemberDto>> GetMembers(int rehearsalId) =>
        await ToDto(BaseQuery(rehearsalId)).ToListAsync();

    private async Task<RehearsalMemberDto?> GetMember(int rehearsalId, int bandMemberId) =>
         await ToDto(BaseQuery(rehearsalId)
            .Where(rm => rm.BandMemberId == bandMemberId))
            .FirstOrDefaultAsync();

    private IQueryable<RehearsalMemberDto> ToDto(IQueryable<RehearsalMember> query) =>
        query
            .OrderBy(rm => rm.RehearsalId)
            .Select(rm => new RehearsalMemberDto(
                rm.BandMember.Id,
                rm.BandMember.User.FirstName + rm.BandMember.User.LastName,
                rm.RehearsalMemberStatus.Name
        ));

    private IQueryable<RehearsalMember> BaseQuery(int rehearsalId) => 
        _db.RehearsalMembers
            .Where(rm => rm.RehearsalId == rehearsalId)
            .AsNoTracking();
}