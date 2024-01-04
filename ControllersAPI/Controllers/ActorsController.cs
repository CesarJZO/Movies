using AutoMapper;
using ControllersAPI.DTOs;
using ControllersAPI.Entities;
using ControllersAPI.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ControllersAPI.Controllers;

[ApiController]
[Route("api/actors")]
public sealed class ActorsController(
    ApplicationDbContext dbContext,
    IMapper mapper,
    IFileStorage fileStorage
) : ControllerBase
{
    private const string ContainerName = "actors";

    private readonly ApplicationDbContext _dbContext = dbContext;
    
    private readonly IMapper _mapper = mapper;

    private readonly IFileStorage _fileStorage = fileStorage;

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] ActorCreationDTO actorCreationDTO)
    {
        var actor = _mapper.Map<Actor>(actorCreationDTO);

        if (actorCreationDTO.Picture is not null)
            actor.Photo = await _fileStorage.SaveFile(ContainerName, actorCreationDTO.Picture);

        _dbContext.Add(actor);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}
