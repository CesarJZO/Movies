using System.ComponentModel.DataAnnotations;
using ControllersAPI.Validations;

namespace ControllersAPI.DTOs;

public readonly struct GenreCreationDTO(string name)
{
    [Required(ErrorMessage = "The '{0}' field is required!")]
    [StringLength(20)]
    [FirstLetterUppercase]
    public string Name { get; init; } = name;
}
