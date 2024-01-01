using AutoMapper;
using ControllersAPI.DTOs;
using ControllersAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControllersAPI.Controllers;

[ApiController] // This attribute automatically validates the model state and returns 400 if it's invalid
[Route("api/[controller]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public sealed class GenresController(
    ILogger<Genre> logger,
    ApplicationDbContext dbContext,
    IMapper mapper
) : ControllerBase
{
    private readonly ILogger<Genre> _logger = logger;
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<GenreDTO[]>> Get()
    {
        _logger.LogInformation("Getting all genres.");

        var genres = await _dbContext.Genres.ToArrayAsync();

        return _mapper.Map<GenreDTO[]>(genres);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GenreDTO>> Get(int id)
    {
        var genre = await _dbContext.Genres.FindAsync(id);

        if (genre is null)
        {
            _logger.LogWarning("Genre with id {id} not found.", id);
            return NotFound();
        }

        _logger.LogInformation("Getting genre with id {id}.", id);
        
        return _mapper.Map<GenreDTO>(genre);
    }

    [HttpPost]
    public async Task<ActionResult<Genre>> Post([FromBody] GenreCreationDTO genreCreationDTO)
    {
        var genre = _mapper.Map<Genre>(genreCreationDTO);
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

