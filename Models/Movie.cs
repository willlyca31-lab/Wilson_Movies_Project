
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wilson_Movies_Project.Models;

public class Movie
{
    public int Id { get; set; }

    // The movie title is required and must contain 3 to 60 characters.
    [Required(ErrorMessage = "The movie title is required.")]
    [StringLength(60, MinimumLength = 3,
        ErrorMessage = "The title must be between 3 and 60 characters.")]
    public string Title { get; set; } = string.Empty;

    // Display the property as "Release Date" and show a date selector.
    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }

    // Genre is required, must start with an uppercase letter,
    // and can contain letters and spaces only.
    [Required(ErrorMessage = "The movie genre is required.")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$",
        ErrorMessage = "Genre must start with an uppercase letter and contain only letters and spaces.")]
    [StringLength(30,
        ErrorMessage = "Genre cannot exceed 30 characters.")]
    public string Genre { get; set; } = string.Empty;

    // Price must be between 1 and 100.
    [Range(1, 100,
        ErrorMessage = "Price must be between 1 and 100.")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    // Rating must be a number from 1 to 5.
    // This preserves the five-star rating system.
    [Required(ErrorMessage = "Please enter a movie rating.")]
    [RegularExpression(@"^[1-5]$",
        ErrorMessage = "Rating must be a number from 1 to 5.")]
    [StringLength(1,
        ErrorMessage = "Rating must contain only one digit.")]
    public string Rating { get; set; } = string.Empty;
}
