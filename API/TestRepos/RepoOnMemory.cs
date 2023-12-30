using API.Entities;
using API;

namespace TestRepos;

internal class RepoOnMemory : IRepo<Genre>
{
    public readonly IEnumerable<Genre> _genres;

    public IEnumerable<Genre> AllGenres => _genres;

    public RepoOnMemory()
    {
        _genres =
        [
            new(1, "Action"),
            new(2, "Adventure"),
            new(3, "Comedy"),
            new(4, "Crime"),
            new(5, "Drama"),
        ];
    }

    public IEnumerable<Genre> Get() => AllGenres;
}
