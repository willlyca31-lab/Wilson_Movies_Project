using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wilson_Movies_Project.Data;
using Wilson_Movies_Project.Models;

namespace Wilson_Movies_Project.Controllers;

public class MoviesController : Controller
{
    private readonly WilsonMoviesContext _context;

    // Inject the database context into the controller.
    public MoviesController(WilsonMoviesContext context)
    {
        _context = context;
    }

    // GET: /Movies
    // Display all movies or filter them by title and genre.
    public async Task<IActionResult> Index(
        string? searchString,
        string? movieGenre)
    {
        // Start with a query that selects all movies.
        var movies = _context.Movie.AsQueryable();

        // Filter movies by title when a search term is provided.
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            movies = movies.Where(m =>
                m.Title.Contains(searchString));
        }

        // Filter movies by genre when a genre is selected.
        if (!string.IsNullOrWhiteSpace(movieGenre))
        {
            movies = movies.Where(m =>
                m.Genre == movieGenre);
        }

        // Retrieve distinct genres and sort them alphabetically.
        var genres = await _context.Movie
            .Select(m => m.Genre)
            .Distinct()
            .OrderBy(genre => genre)
            .ToListAsync();

        // Pass the genre list to the view.
        ViewBag.Genres = genres;

        // Preserve the search values in the form.
        ViewBag.SearchString = searchString;
        ViewBag.MovieGenre = movieGenre;

        // Execute the filtered query.
        var movieList = await movies.ToListAsync();

        // Send the filtered movies to the Index view.
        return View(movieList);
    }

    // GET: /Movies/Details/1
    // Retrieve one movie using its ID.
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: /Movies/Create
    // Display the form for creating a movie.
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Movies/Create
    // Bind only the permitted movie properties to prevent overposting.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
    {
        if (!ModelState.IsValid)
        {
            return View(movie);
        }

        _context.Movie.Add(movie);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: /Movies/Edit/1
    // Retrieve the movie that will be edited.
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: /Movies/Edit/1
    // Update the movie using only the allowed properties.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(movie);
        }

        _context.Attach(movie).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Check whether the movie still exists in the database.
            if (!MovieExists(movie.Id))
            {
                return NotFound();
            }

            // Re-throw the exception if the movie still exists.
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: /Movies/Delete/1
    // Display the confirmation page before deleting a movie.
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: /Movies/Delete/1
    // Delete the selected movie after confirmation.
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _context.Movie.FindAsync(id);

        if (movie != null)
        {
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    // Check whether a movie with the specified ID exists.
    private bool MovieExists(int id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}