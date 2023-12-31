using System.ComponentModel.DataAnnotations;
using ControllersAPI.Validations;

namespace ControllersAPI.Entities;

/// <summary>
/// Represents a genre of a movie.
/// </summary>
public sealed record Genre
{
    public int Id { get; init; }

    [Required(ErrorMessage = "The '{0}' field is required.")]
    [StringLength(10)]
    [FirstLetterUppercase]
    public string Name { get; set; } = string.Empty;
}
