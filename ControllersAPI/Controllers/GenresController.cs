using ControllersAPI.Entities;
using ControllersAPI.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControllersAPI.Controllers;

[ApiController] // This attribute automatically validates the model state and returns 400 if it's invalid
[Route("api/[controller]")]
public sealed class GenresController(ILogger<Genre> logger, IRepo<Genre> repo) : ControllerBase
{
    private readonly IRepo<Genre> _repo = repo;
    private readonly ILogger<Genre> _logger = logger;

    [HttpGet]
    [HttpGet("all")]
    // [ResponseCache(Duration = 60)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Genre>> Get()
    {
        _logger.LogInformation("Getting all genres: {GA}", _repo.GetAll);
        return (List<Genre>)_repo.GetAll;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Genre> Get(int id, [FromHeader] string Name)
    {
        _logger.LogInformation("Getting genre by id");
        // This is not needed because of the ApiController attribute
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest(ModelState);
        // }

        Genre genre = _repo.Get(id);

        if (genre is null)
        {
            _logger.LogWarning("Genre not found");
            return NotFound();
        }

        return genre;
    }

    [HttpGet("async/{id:int}")]
    public async Task<ActionResult<Genre>> GetAsync(int id)
    {
        Genre genre = await _repo.GetAsync(id);

        if (genre is null)
            return NotFound();

        return genre;
    }

    [HttpGet("guid")]
    public ActionResult<Guid> GetGuid()
    {
        _logger.LogInformation("Getting guid");
        if (_repo is null)
        {
            _logger.LogWarning("Repo is null");
            return NotFound();
        }
        return _repo.Guid;
    }

    [HttpPost]
    public Genre Post([FromBody] Genre genre)
    {
        var newGenre = genre with { Id = _repo.GetAll.Count() + 1 };
        return newGenre;
    }

    [HttpPut]
    public Genre Put(Genre genre)
    {
        return genre;
    }

    [HttpDelete]
    public Genre Delete(Genre genre)
    {
        return genre;
    }
}

