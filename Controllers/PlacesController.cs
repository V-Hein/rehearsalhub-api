
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/places")]
public class PlacesController : ControllerBase
{
    private readonly AppDbContext _db;

    public PlacesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Place>>> GetAll()
    {
        var places = await _db.Places
            .Select(p => new PlaceDto(
                p.Id,
                p.Name,
                p.Address
            )).ToListAsync();

        if (places == null)
            return NotFound();

        return Ok(places);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Place>> GetById(int id)
    {
        var place = await _db.Places
            .Where(p => p.Id == id)
            .Select(p => new PlaceDto(
                p.Id,
                p.Name,
                p.Address
            )).FirstOrDefaultAsync();

        if (place == null)
            return NotFound();

        return Ok(place);
    }

    [HttpPost]
    public async Task<ActionResult<Place>> Create(CreatePlaceDto dto)
    {
        var place = new Place
        {
            Name = dto.Name,
            Address = dto.Address
        };

        _db.Places.Add(place);

        await _db.SaveChangesAsync();

        var result = await _db.Places
            .Where(p => p.Id == place.Id)
            .Select(p => new PlaceDto(
                p.Id,
                p.Name,
                p.Address
            )).FirstOrDefaultAsync();

        return CreatedAtAction(nameof(GetById), new { id = place.Id }, result);
    }
}