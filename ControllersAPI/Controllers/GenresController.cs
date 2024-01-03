using AutoMapper;
using ControllersAPI.DTOs;
using ControllersAPI.Entities;
using ControllersAPI.Utils;
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
    public async Task<ActionResult<GenreDTO[]>> Get([FromQuery] PaginationDTO paginationDTO)
    {
        var queryableGenres = _dbContext.Genres.AsQueryable();
        await HttpContext.InsertPaginationParamsInHeader(queryableGenres);

        var genres = await queryableGenres
            .OrderBy(g => g.Name)
            .Paginate(paginationDTO)
            .ToArrayAsync();

        _logger.LogInformation(
            "Getting all genres: [{G}].",
            string.Join(", ", queryableGenres.Select(g => g.Name))
        );

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
    public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreationDTO)
    {
        var genre = _mapper.Map<Genre>(genreCreationDTO);
        _dbContext.Add(genre);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, GenreCreationDTO genreCreationDTO)
    {
        var genre = await _dbContext.Genres.FindAsync(id);

        if (genre is null)
        {
            _logger.LogWarning("Genre with id {id} not found.", id);
            return NotFound();
        }

        var originalName = genre.Name;

        // genreUpdated is the same instance as genre
        var genreUpdated = _mapper.Map(genreCreationDTO, genre);
        // _dbContext.Update(genreUpdated); // This may not be necessary

        _logger.LogInformation("Updating genre with id {id} from '{og}' to '{new}'.", id, originalName, genre.Name);

        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        bool exists = await _dbContext.Genres.AnyAsync(g => g.Id == id);

        if (!exists)
        {
            _logger.LogWarning("Genre with id {id} not found.", id);
            return NotFound();
        }

        _dbContext.Remove(new Genre { Id = id });

        _logger.LogInformation("Deleting genre with id {id}.", id);

        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}

