using ControllersAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControllersAPI;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    private readonly DbContextOptions _options = options;

    public DbSet<Genre> Genres { get; set; }
}
