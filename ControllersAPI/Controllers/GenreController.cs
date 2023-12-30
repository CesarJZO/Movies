using ControllersAPI.Entities;
using ControllersAPI.Repos;
using Microsoft.AspNetCore.Mvc;

namespace ControllersAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController(IRepo<Genre> repo) : ControllerBase
{
    [HttpGet(Name = "GetGenre")]
    public IEnumerable<Genre> Get()
    {
        return repo.GetAll;
    }
}

