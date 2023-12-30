using System.ComponentModel.DataAnnotations;

namespace ControllersAPI.Entities;

/// <summary>
/// Represents a genre of a movie.
/// </summary>
public sealed record Genre
{
    public int Id { get; init; }

    [Required(ErrorMessage = "The '{0}' field is required!")][StringLength(10)]
    public string Name { get; set; } = string.Empty;
}
