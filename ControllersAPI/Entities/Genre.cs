using System.ComponentModel.DataAnnotations;
using ControllersAPI.Validations;

namespace ControllersAPI.Entities;

/// <summary>
/// Represents a genre of a movie.
/// </summary>
public sealed record Genre : IValidatableObject
{
    public int Id { get; init; }

    [Required(ErrorMessage = "The '{0}' field is required!")]
    [StringLength(10)]
    // [FirstLetterUppercase]
    public string Name { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        ReadOnlySpan<char> span = Name;

        if (!char.IsUpper(span[0]))
            yield return new ValidationResult("First letter must be uppercase.", memberNames: [ nameof(Name) ]);
    }
}
