
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/places")]
[Authorize]
public class PlacesController : ControllerBase
{
    private readonly AppDbContext _db;
    public PlacesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Place>>> GetAll()
    {
        var places = await GetPlaces();
        return Ok(places);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Place>> GetById(int id)
    {
        var place = await GetPlace(id);
        return place == null ? NotFound() : Ok(place);
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

        var result = await GetPlace(place.Id);

        return CreatedAtAction(nameof(GetById), new { id = place.Id }, result);
    }

    private async Task<List<PlaceDto>> GetPlaces() =>
        await ToDto(BaseQuery()).ToListAsync();

    private async Task<PlaceDto?> GetPlace(int id) =>
        await ToDto(BaseQuery().Where(p => p.Id == id)).FirstOrDefaultAsync();

    private IQueryable<PlaceDto> ToDto(IQueryable<Place> query) =>
        query
            .OrderBy(p => p.Id)
            .Select(p => new PlaceDto(
                p.Id,
                p.Name,
                p.Address
            ));

    private IQueryable<Place> BaseQuery() => _db.Places.AsNoTracking();
}