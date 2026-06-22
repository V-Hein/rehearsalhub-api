

using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/bands/{bandId:int}/members")]
public class BandMembersController : ControllerBase
{
    private readonly AppDbContext _db;

    public BandMembersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BandMember>>> GetAll(int bandId)
    {
        var members = await _db.BandMembers
            .Where(bm => bm.BandId == bandId)
            .Select(bm => new BandMemberDto(
                bm.Id,
                bm.BandId,
                bm.UserId,
                bm.User.FirstName + bm.User.LastName,
                bm.Role.Name
            )).ToListAsync();

        if (members == null)
            return NotFound();

        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BandMember>> GetById(int bandId, int id)
    {
        var member = await _db.BandMembers
            .Where(bm => bm.BandId == bandId && bm.Id == id)
            .Select(bm => new BandMemberDto(
                bm.Id,
                bm.BandId,
                bm.UserId,
                bm.User.FirstName + bm.User.LastName,
                bm.Role.Name
            )).FirstOrDefaultAsync();

        if (member == null)
            return NotFound();

        return Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult<BandMember>> Create(int bandId, CreateBandMemberDto dto)
    {
        var member = new BandMember
        {
            BandId = bandId,
            UserId = dto.UserId,
            RoleId = dto.RoleId
        };

        _db.BandMembers.Add(member);

        await _db.SaveChangesAsync();

        var result = await _db.BandMembers
            .Where(bm => bm.BandId == bandId && bm.UserId == dto.UserId)
            .Select(bm => new BandMemberDto(
                bm.Id,
                bm.BandId,
                bm.UserId,
                bm.User.FirstName + bm.User.LastName,
                bm.Role.Name
            )).FirstOrDefaultAsync();
        
        return CreatedAtAction(nameof(GetById), new { bandId = member.BandId, id = member.Id }, result);
    }
}