using ControllersAPI.Entities;

namespace ControllersAPI.Repos;

public class RepoOnMemory : IRepo<Genre>
{
    private static readonly string[] Genres =
    [
        "Action", "Adventure", "Comedy", "Crime", "Drama", "Fantasy", "Historical", "Historical fiction", "Horror", "Magical realism", "Mystery", "Paranoid fiction", "Philosophical", "Political", "Romance", "Saga", "Satire", "Science fiction", "Social", "Speculative", "Thriller", "Urban", "Western"
    ];

    public IEnumerable<Genre> GetAll => Enumerable.Range(1, 5)
        .Select(index => new Genre(
            Id: index,
            Name: Genres[Random.Shared.Next(Genres.Length)]
        ))
        .ToArray();
}
