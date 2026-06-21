

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/genres")]
public class GenresController : ControllerBase
{
    private readonly AppDbContext _db;

    public GenresController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetAll()
    {
        var genres = await _db.Genres
            .Select(g => new GenreDto(
                g.Id,
                g.Name
            )).ToListAsync();

        if (genres == null)
            return NotFound();

        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Genre>> GetById(int id)
    {
        var genre = await _db.Genres
            .Where(g => g.Id == id)
            .Select(g => new GenreDto(
                g.Id,
                g.Name
            )).FirstOrDefaultAsync();

        if (genre == null)
            return NotFound();

        return Ok(genre);
    }

    [HttpPost]
    public async Task<ActionResult<Genre>> Create(CreateGenreDto dto)
    {
        var genre = new Genre
        {
            Name = dto.Name
        };

        _db.Genres.Add(genre);

        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = genre.Id}, genre);
    }
}