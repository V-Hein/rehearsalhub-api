
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/instrument_types")]
[Authorize]
public class InstrumentTypesController : ControllerBase
{
    private readonly AppDbContext _db;
    public InstrumentTypesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InstrumentType>>> GetAll()
    {
        var types = await GetTypes();
        return Ok(types);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InstrumentType>> GetById(int id)
    {
        var type = await GetType(id);
        return type == null ? NotFound() : Ok(type);
    }

    [HttpPost]
    public async Task<ActionResult<InstrumentType>> Create(CreateInstrumentTypeDto dto)
    {
        var type = new InstrumentType{ Name = dto.Name };

        _db.InstrumentTypes.Add(type);
        await _db.SaveChangesAsync();

        var result = await GetType(type.Id);

        return CreatedAtAction(nameof(GetById), new { id = type.Id }, result);
    }

    private async Task<List<InstrumentTypeDto>> GetTypes() =>
        await ToDto(BaseQuery()).ToListAsync();

    private async Task<InstrumentTypeDto?> GetType(int id) =>
        await ToDto(BaseQuery().Where(t => t.Id == id)).FirstOrDefaultAsync();

    private IQueryable<InstrumentTypeDto> ToDto(IQueryable<InstrumentType> query) 
        => query
            .OrderBy(t => t.Id)
            .Select(t => new InstrumentTypeDto(
                t.Id,
                t.Name
            ));

    private IQueryable<InstrumentType> BaseQuery() => _db.InstrumentTypes.AsNoTracking();
}