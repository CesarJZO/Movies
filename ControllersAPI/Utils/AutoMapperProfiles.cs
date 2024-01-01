using AutoMapper;
using ControllersAPI.DTOs;
using ControllersAPI.Entities;

namespace ControllersAPI.Utils;

public sealed class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Genre, GenreDTO>().ReverseMap();
    }
}
