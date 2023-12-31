using ControllersAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ControllersAPI.Controllers;

[ApiController] // This attribute automatically validates the model state and returns 400 if it's invalid
[Route("api/[controller]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public sealed class GenresController(ILogger<Genre> logger) : ControllerBase
{
    private readonly ILogger<Genre> _logger = logger;

    [HttpGet]
    public ActionResult<IEnumerable<Genre>> Get()
    {
        return (List<Genre>)
        [
            new Genre { Id = 1, Name = "Comedy" },
            new Genre { Id = 2, Name = "Action" }
        ];
    }

    [HttpGet("{id:int}")]
    public ActionResult<Genre> Get(int id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("async/{id:int}")]
    public async Task<ActionResult<Genre>> GetAsync(int id)
    {
        await Task.Delay(1000);
        throw new NotImplementedException();
    }

    [HttpPost]
    public Genre Post([FromBody] Genre genre)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public Genre Put(Genre genre)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public Genre Delete(Genre genre)
    {
        throw new NotImplementedException();
    }
}

