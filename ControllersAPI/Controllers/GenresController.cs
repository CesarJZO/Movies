using ControllersAPI.Entities;
using ControllersAPI.Repos;
using Microsoft.AspNetCore.Mvc;

namespace ControllersAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class GenresController(IRepo<Genre> repo) : ControllerBase
{
    private readonly IRepo<Genre> _repo = repo;

    [HttpGet]
    [HttpGet("all")]
    public IEnumerable<Genre> Get()
    {
        return _repo.GetAll;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Genre> Get(int id)
    {
        Genre genre = _repo.Get(id);

        if (genre is null)
            return NotFound();

        return genre;
    }

    [HttpPost]
    public Genre Post(Genre genre)
    {
        return genre with { Id = _repo.GetAll.Count() + 1 };
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

