using Wilson_Movies_Project.Data;

namespace Wilson_Movies_Project.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        // Retrieve all the database context from dependency injection.
        var context = serviceProvider.GetRequiredService<WilsonMoviesContext>();

        // Check whether the database already contains movies.
        // Do not insert duplicate sample records.
        if (context.Movie.Any())
        {
            return;
        }

        // Add the sample movies provided in the Microsoft tutorial.
        context.Movie.AddRange(
            new Movie
            {
                Title = "When Harry Met Sally",
                ReleaseDate = new DateTime(1989, 2, 12),
                Genre = "Romantic Comedy",
                Price = 7.99M,
                Rating = "R"
            },

            new Movie
            {
                Title = "Ghostbusters",
                ReleaseDate = new DateTime(1984, 3, 13),
                Genre = "Comedy",
                Price = 8.99M,
                Rating = "PG"
            },

            new Movie
            {
                Title = "Ghostbusters 2",
                ReleaseDate = new DateTime(1986, 2, 23),
                Genre = "Comedy",
                Price = 9.99M,
                Rating = "PG"
            },

            new Movie
            {
                Title = "Rio Bravo",
                ReleaseDate = new DateTime(1959, 4, 15),
                Genre = "Western",
                Price = 3.99M,
                Rating = "Approved"
            },

            new Movie
            {
                Title = "Jaws",
                ReleaseDate = new DateTime(1975, 6, 20),
                Genre = "Thriller",
                Price = 9.99M,
                Rating = "4"
            },

            new Movie
            {
                Title = "The Matrix",
                ReleaseDate = new DateTime(1999, 3, 31),
                Genre = "Science Fiction",
                Price = 10.99M,
                Rating = "5"
            },

            new Movie
            {
                Title = "Inception",
                ReleaseDate = new DateTime(2010, 7, 16),
                Genre = "Action",
                Price = 12.99M,
                Rating = "5"
            }
        );

        // Save all sample movie records to the database.
        context.SaveChanges();
    }
}