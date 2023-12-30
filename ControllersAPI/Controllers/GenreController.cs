using ControllersAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ControllersAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController() : ControllerBase
{
    private static readonly string[] Genres =
    [
        "Action", "Adventure", "Comedy", "Crime", "Drama", "Fantasy", "Historical", "Historical fiction", "Horror", "Magical realism", "Mystery", "Paranoid fiction", "Philosophical", "Political", "Romance", "Saga", "Satire", "Science fiction", "Social", "Speculative", "Thriller", "Urban", "Western"
    ];


    [HttpGet(Name = "GetGenre")]
    public IEnumerable<Genre> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Genre(
            Id: index,
            Name: Genres[Random.Shared.Next(Genres.Length)]))
            .ToArray();
    }
}

