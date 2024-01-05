namespace ControllersAPI.DTOs;

public struct ActorDTO
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Biography { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Piture { get; set; }
}
