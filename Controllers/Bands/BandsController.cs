

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/bands")]
public class BandsController : ControllerBase
{
    private readonly AppDbContext _db;

    public BandsController(AppDbContext db)
    {
        _db = db;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Band>>> GetAll()
    {
        var bands = await _db.Bands
            .Select(b => new BandDto(
                b.Id,
                b.Name,
                b.BandGenres
                    .Select(bg => bg.Genre.Name)
                    .ToList()
            )).ToListAsync();

        if (bands == null)
            return NotFound();

        return Ok(bands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Band>> GetById(int id)
    {
        var band = await _db.Bands
            .Where(b => b.Id == id)
            .Select(b => new BandDto(
                b.Id,
                b.Name,
                b.BandGenres
                    .Select(bg => bg.Genre.Name)
                    .ToList()
            )).FirstOrDefaultAsync();

        if (band == null)
            return NotFound();

        return Ok(band);
    }

    [HttpPost]
    public async Task<ActionResult<Band>> Create(CreateBandDto dto)
    {
        var band = new Band
        {
            Name = dto.Name
        };

        foreach (var genreId in dto.GenreIds)
        {
            band.BandGenres.Add(new BandGenre
            {
                GenreId = genreId
            });
        }

        _db.Bands.Add(band);

        await _db.SaveChangesAsync();

        var result = await _db.Bands
            .Where(b => b.Id == band.Id)
            .Select(b => new BandDto(
                b.Id,
                b.Name,
                b.BandGenres
                    .Select(bg => bg.Genre.Name)
                    .ToList()
            )).FirstOrDefaultAsync();


        return CreatedAtAction(nameof(GetById), new { id = band.Id }, result);
    }

    [HttpPost("bulk")]
    public async Task<ActionResult<IEnumerable<Band>>> CreateMany(List<CreateBandDto> dtos)
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
                    .Select(bg => bg.Genre.Name)
                    .ToList()
            )).ToListAsync();

        return Created("", result);
    }
}