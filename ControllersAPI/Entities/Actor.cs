namespace ControllersAPI.Entities;

public sealed record Actor
{
    public int Id { get; init; }

    public required string Name { get; set; }

    public string? Biography { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? Photo { get; set; }
}
