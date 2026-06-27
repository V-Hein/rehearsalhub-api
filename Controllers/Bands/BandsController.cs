

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/bands")]
[Authorize]
public class BandsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IUserContext _user;
    private int UserId => _user.UserId;

    public BandsController(AppDbContext db, IUserContext user)
    {
        _db = db;
        _user = user;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BandDto>>> GetAll()
    {
        var bands = await GetBands();
        return Ok(bands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BandDto>> GetById(int id)
    {
        var band = await GetBand(id);
        return band == null ? NotFound() : Ok(band);
    }

    [HttpPost]
    public async Task<ActionResult<BandDto>> Create(CreateBandDto dto)
    {
        var band = new Band { Name = dto.Name };

        foreach (var genreId in dto.GenreIds)
            band.BandGenres.Add(new BandGenre
            {
                GenreId = genreId
            });
        
        _db.Bands.Add(band);
        await _db.SaveChangesAsync();

        var result = await GetBand(band.Id);

        return CreatedAtAction(nameof(GetById), new { id = band.Id }, result);
    }

    [HttpPost("bulk")]
    public async Task<ActionResult<IEnumerable<BandDto>>> CreateMany(List<CreateBandDto> dtos)
    {
        var bands = dtos.Select(dto => new Band
        {
            Name = dto.Name,
            BandGenres = dto.GenreIds
                .Select(id => new BandGenre
                {
                    GenreId = id
                })
                .ToList()
        }).ToList();

        _db.Bands.AddRange(bands);
        
        await _db.SaveChangesAsync();

        var createdIds = bands.Select(b => b.Id).ToList();

        var result = await _db.Bands
            .Where(b => createdIds.Contains(b.Id))
            .Select(b => new BandDto(
                b.Id,
                b.Name,
                b.BandGenres
                    .Select(bg => bg.Genre.Id)
                    .ToList(),
                b.BandGenres
                    .Select(bg => bg.Genre.Name)
                    .ToList()
            )).ToListAsync();

        return Created("", result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BandDto>> Update(int id, CreateBandDto dto)
    {
        var band = await _db.Bands
            .Include(b => b.BandGenres)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (band == null)
            return NotFound();

        band.Name = dto.Name;
        band.BandGenres.Clear();

        foreach (var genreId in dto.GenreIds)
            band.BandGenres.Add(new BandGenre
            {
                BandId = band.Id,
                GenreId = genreId
            });

        await _db.SaveChangesAsync();

        return Ok(await GetBand(id));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (hasSongs(id)) 
            return Problem(
                detail: "Band cannot be deleted because it own songs.",
                statusCode: StatusCodes.Status409Conflict,
                title: "Resource Conflict"
            );

        var band = await _db.Bands.FindAsync(id);

        if (band == null)
            return NotFound();

        _db.Bands.Remove(band);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    private async Task<List<BandDto>> GetBands() =>
        await ToDto(BaseQuery()).ToListAsync();

    private async Task<BandDto?> GetBand(int id) =>
        await ToDto(BaseQuery().Where(b => b.Id == id)).FirstOrDefaultAsync();

    private IQueryable<BandDto> ToDto(IQueryable<Band> query) =>
        query
            .OrderBy(b => b.Id)
            .Select(b => new BandDto(
                b.Id,
                b.Name,
                b.BandGenres
                    .Select(bg => bg.Genre.Id)
                    .ToList(),
                b.BandGenres
                    .Select(bg => bg.Genre.Name)
                    .ToList()
            ));

    private IQueryable<Band> BaseQuery() => 
        _db.Bands
            // .Where(b => b.BandMembers.Any(m => m.UserId == UserId))
            .AsNoTracking();

    private bool hasSongs(int id) =>
        _db.Songs.Any(s => s.BandId == id);
}