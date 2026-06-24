using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/ratings")]
[Authorize]
public class RatingsController : ControllerBase
{
    private readonly AppDbContext _db;
    public RatingsController(AppDbContext db) =>_db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Rating>>> GetAll()
    {
        var ratings = await GetRatings();
        return Ok(ratings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Rating>> GetById(int id)
    {
        var rating = await GetRating(id);
        return rating == null ? NotFound() : Ok(rating);
    }

    [HttpPost]
    public async Task<ActionResult<RatingDto>> Create(CreateRatingDto dto)
    {
        var rating = new Rating{ Name = dto.Name };

        _db.Ratings.Add(rating);
        await _db.SaveChangesAsync();

        var result = await GetRating(rating.Id);

        return CreatedAtAction(nameof(GetById), new { id = rating.Id}, result);
    }

    private async Task<List<RatingDto>> GetRatings() =>
        await ToDto(BaseQuery()).ToListAsync();
    private async Task<RatingDto?> GetRating(int id) => 
        await ToDto(BaseQuery().Where(rs => rs.Id == id)).FirstOrDefaultAsync();

    private IQueryable<RatingDto> ToDto(IQueryable<Rating> query) =>
        query
            .OrderBy(rs => rs.Id)
            .Select(rs => new RatingDto(
                rs.Id,
                rs.Name
            ));
    
    private IQueryable<Rating> BaseQuery() => _db.Ratings.AsNoTracking();
}