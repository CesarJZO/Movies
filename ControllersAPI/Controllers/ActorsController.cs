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
    ILogger<Actor> logger,
    IMapper mapper,
    IFileStorage fileStorage
) : ControllerBase
{
    private const string ContainerName = "actors";

    private readonly ApplicationDbContext _dbContext = dbContext;

    private readonly ILogger<Actor> _logger = logger;

    private readonly IMapper _mapper = mapper;

    private readonly IFileStorage _fileStorage = fileStorage;

    [HttpGet]
    public async Task<ActionResult<ActorDTO[]>> Get([FromQuery] PaginationDTO pagination)
    {
        IQueryable<Actor> queriable = _dbContext.Actors.AsQueryable();

        await HttpContext.InsertPaginationParamsInHeader(queriable);

        var actors = queriable.OrderBy(a => a.Name).Paginate(pagination);

        return _mapper.Map<ActorDTO[]>(actors);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ActorDTO>> Get(int id)
    {
        Actor? actor = await _dbContext.Actors.FindAsync(id);

        if (actor is null)
        {
            _logger.LogWarning("Actor with id {id} not found.", id);
            return NotFound();
        }

        _logger.LogInformation("Getting actor {name} with id {id}.", actor.Name, id);
        return _mapper.Map<ActorDTO>(actor);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromForm] ActorCreationDTO actorCreationDTO)
    {
        var actor = _mapper.Map<Actor>(actorCreationDTO);

        if (actorCreationDTO.Picture is not null)
            actor.Photo = await _fileStorage.SaveFile(ContainerName, actorCreationDTO.Picture);

        _logger.LogInformation("Creating actor {name}.", actor.Name);

        _dbContext.Add(actor);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromForm] ActorCreationDTO actorCreationDTO)
    {
        Actor? actor = await _dbContext.Actors.FindAsync(id);

        if (actor is null)
        {
            _logger.LogWarning("Actor with id {id} not found.", id);
            return NotFound();
        }

        actor = _mapper.Map(actorCreationDTO, actor);

        if (actorCreationDTO is { Picture: not null })
        {
            if (actor.Photo is not null)
                actor.Photo = await _fileStorage.EditFile(ContainerName, actorCreationDTO.Picture, actor.Photo);
            else
                actor.Photo = await _fileStorage.SaveFile(ContainerName, actorCreationDTO.Picture);
        }

        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        Actor? actor = await _dbContext.Actors.FindAsync(id);

        if (actor is null)
        {
            _logger.LogWarning("Actor with id {id} not found.", id);
            return NotFound();
        }

        _logger.LogInformation("Deleting actor {name} with id {id}.", actor.Name, id);

        _dbContext.Remove(actor);
        await _dbContext.SaveChangesAsync();

        if (actor.Photo is not null)
        {
            _logger.LogInformation("Deleting actor {name} photo.", actor.Name);
            await _fileStorage.DeleteFile(actor.Photo, ContainerName);
        }

        return NoContent();
    }
}
