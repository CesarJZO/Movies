using AutoMapper;
using ControllersAPI.DTOs;
using ControllersAPI.Entities;

namespace ControllersAPI.Utils;

public sealed class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Genre, GenreDTO>().ReverseMap();
        CreateMap<GenreCreationDTO, Genre>();
        CreateMap<Actor, ActorDTO>().ReverseMap();
        CreateMap<ActorCreationDTO, Actor>()
            .ForMember(actor => actor.Photo, options => options.Ignore());
    }
}
