

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/genres")]
[Authorize]
public class GenresController : ControllerBase
{
    private readonly AppDbContext _db;
    public GenresController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll()
    {
        var genres = await GetGenres();
        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreDto>> GetById(int id)
    {
        var genre = await GetGenre(id);
        return genre == null ? NotFound() : Ok(genre);
    }

    [HttpPost]
    public async Task<ActionResult<GenreDto>> Create(CreateGenreDto dto)
    {
        var genre = new Genre { Name = dto.Name };

        _db.Genres.Add(genre);
        await _db.SaveChangesAsync();

        var result = await GetGenre(genre.Id);

        return CreatedAtAction(nameof(GetById), new { id = genre.Id}, result);
    }

    private async Task<List<GenreDto>> GetGenres() =>
        await ToDto(BaseQuery()).ToListAsync();

    private async Task<GenreDto?> GetGenre(int id) =>
        await ToDto(BaseQuery().Where(g => g.Id == id)).FirstOrDefaultAsync();

    private IQueryable<GenreDto> ToDto(IQueryable<Genre> query) =>
        query
            .OrderBy(g => g.Id)
            .Select(g => new GenreDto(
                g.Id,
                g.Name
            ));

    private IQueryable<Genre> BaseQuery() => _db.Genres.AsNoTracking();
}