using ControllersAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControllersAPI.Controllers;

[ApiController] // This attribute automatically validates the model state and returns 400 if it's invalid
[Route("api/[controller]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public sealed class GenresController(ILogger<Genre> logger, ApplicationDbContext dbContext) : ControllerBase
{
    private readonly ILogger<Genre> _logger = logger;
    private readonly ApplicationDbContext _dbContext = dbContext;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> Get()
    {
        _logger.LogInformation("Getting all genres.");
        return await _dbContext.Genres.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Genre>> Get(int id)
    {
        var genre = await _dbContext.Genres.FindAsync(id);

        if (genre is null)
        {
            _logger.LogWarning("Genre with id {id} not found.", id);
            return NotFound();
        }

        _logger.LogInformation("Getting genre with id {id}.", id);
        return genre;
    }

    [HttpPost]
    public async Task<ActionResult<Genre>> Post([FromBody] Genre genre)
    {
        _dbContext.Add(genre);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult<Genre>> Put(Genre genre)
    {
        _dbContext.Update(genre);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult<Genre>> Delete(Genre genre)
    {
        _dbContext.Remove(genre);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}

