using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public class GenreController(IRepo<Genre> repo)
{
    private readonly IRepo<Genre> _repo = repo;

    [HttpGet]
    public IEnumerable<Genre> Get()
    {
        return _repo.Get();
    }

    [HttpPost]
    public void Post([FromBody] Genre genre)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public void Put([FromBody] Genre genre)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public void Delete([FromBody] Genre genre)
    {
        throw new NotImplementedException();
    }
}
