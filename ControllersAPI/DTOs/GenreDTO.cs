namespace ControllersAPI.DTOs;

public struct GenreDTO(int id, string name)
{
    public int Id { get; init; } = id;
    public string Name { get; set; } = name;
}
