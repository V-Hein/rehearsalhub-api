using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/rehearsals/{rehearsalId:int}/members")]
public class RehearsalMembersController : ControllerBase
{
    private readonly AppDbContext _db;

    public RehearsalMembersController(AppDbContext db)
    {
        _db = db;
    }

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

        if (member == null)
            return NotFound();

        return Ok(member);
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

    private async Task<List<RehearsalMemberDto>> GetMembers(int rehearsalId)
    {
        return await ToDto(BaseQuery()
            .Where(rm => rm.RehearsalId == rehearsalId))
            .ToListAsync();
    }

    private async Task<RehearsalMemberDto?> GetMember(int rehearsalId, int bandMemberId)
    {
        return await ToDto(BaseQuery()
        .Where(rm => rm.RehearsalId == rehearsalId && rm.BandMemberId == bandMemberId))
        .FirstOrDefaultAsync();
    }

    private IQueryable<RehearsalMemberDto> ToDto(IQueryable<RehearsalMember> query)
    {
        return query
            .OrderBy(rm => rm.RehearsalId)
            .Select(rm => new RehearsalMemberDto(
            rm.BandMember.Id,
            rm.BandMember.User.FirstName + rm.BandMember.User.LastName,
            rm.RehearsalMemberStatus.Name
        ));
    }

    private IQueryable<RehearsalMember> BaseQuery()
    {
        return _db.RehearsalMembers;
    }

}