using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Dynamic;

[ApiController]
[Route("api/bands/{bandId:int}/members")]
[Authorize]
public class BandMembersController : ControllerBase
{
    private readonly AppDbContext _db;
    public BandMembersController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BandMember>>> GetAll(int bandId)
    {
        var members = await GetMembers(bandId);
        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BandMember>> GetById(int bandId, int id)
    {
        var member = await GetMember(bandId, id);
        return Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult<BandMember>> Create(int bandId, CreateBandMemberDto dto)
    {
        var member = new BandMember
        {
            BandId = bandId,
            UserId = dto.UserId,
            BandRoleId = dto.RoleId
        };

        _db.BandMembers.Add(member);
        await _db.SaveChangesAsync();

        var result = await GetMember(bandId, member.Id);
        
        return CreatedAtAction(nameof(GetById), new { bandId = member.BandId, id = member.Id }, result);
    }

    private async Task<List<BandMemberDto>> GetMembers(int bandId) =>
        await ToDto(BaseQuery(bandId)).ToListAsync();

    private async Task<BandMemberDto?> GetMember(int bandId, int id) =>
        await ToDto(BaseQuery(bandId).Where(bm => bm.Id == id)).FirstOrDefaultAsync();

    private IQueryable<BandMemberDto> ToDto(IQueryable<BandMember> query) =>
        query
            .OrderBy(m => m.Id)
            .Select(m => new BandMemberDto(
                m.Id,
                m.BandId,
                m.User.Id,
                m.BandRoleId,
                m.Band.Name,
                m.User.FirstName + " " + m.User.LastName,
                m.BandRole.Name
            ));

    private IQueryable<BandMember> BaseQuery(int bandId) => 
        _db.BandMembers
            .Where(m => m.BandId == bandId)
            .AsNoTracking();
}