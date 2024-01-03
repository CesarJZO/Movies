using System.ComponentModel.DataAnnotations;

namespace ControllersAPI.DTOs;

public readonly struct ActorCreationDTO
{
    [Required]
    [StringLength(120)]
    public string Name { get; init; }

    public string? Biography { get; init; }
    
    public DateTime DateOfBirth { get; init; }
}
