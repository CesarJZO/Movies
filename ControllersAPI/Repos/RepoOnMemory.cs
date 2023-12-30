using ControllersAPI.Entities;

namespace ControllersAPI.Repos;

public class RepoOnMemory : IRepo<Genre>
{
    private readonly List<Genre> _genres;

    private static readonly string[] GenreNames =
    [
        "Action", "Adventure", "Comedy", "Crime", "Drama", "Fantasy", "Historical", "Historical fiction", "Horror", "Magical realism", "Mystery", "Paranoid fiction", "Philosophical", "Political", "Romance", "Saga", "Satire", "Science fiction", "Social", "Speculative", "Thriller", "Urban", "Western"
    ];

    public IEnumerable<Genre> GetAll => _genres;

    public Guid Guid { get; init; }

    public RepoOnMemory()
    {
        _genres = Enumerable.Range(1, 3)
            .Select(index => new Genre
            {
                Id = index,
                Name = GenreNames[Random.Shared.Next(GenreNames.Length)]
            })
            .ToList();

        Guid = Guid.NewGuid();
    }

    public Genre Get(int id)
    {
        return _genres.FirstOrDefault(g => g.Id == id)!;
    }

    public async Task<Genre> GetAsync(int id)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));

        return _genres.FirstOrDefault(g => g.Id == id)!;
    }

    public Genre Post(Genre entity)
    {
        var newGenre = entity with { Id = _genres.Count + 1 };
        _genres.Add(newGenre);
        return newGenre;
    }
}
