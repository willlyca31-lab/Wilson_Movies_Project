
using Microsoft.EntityFrameworkCore;
using Wilson_Movies_Project.Models;

namespace Wilson_Movies_Project.Data;

public class WilsonMoviesContext : DbContext
{
    // Receives the database configuration through dependency injection.
    public WilsonMoviesContext(DbContextOptions<WilsonMoviesContext> options)
        : base(options)
    {
    }

    // Represents the Movies table in the SQLite database.
    public DbSet<Movie> Movie { get; set; } = default!;
}
