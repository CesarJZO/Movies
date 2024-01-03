using AutoMapper;
using ControllersAPI.DTOs;
using ControllersAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ControllersAPI.Controllers;

[ApiController]
[Route("api/actors")]
public sealed class ActorsController(ApplicationDbContext dbContext, IMapper mapper) : ControllerBase
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ActorCreationDTO actorCreationDTO)
    {
        var actor = _mapper.Map<Actor>(actorCreationDTO);
        _dbContext.Add(actor);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}
