using AutoMapper;
using ChatApp.API.Models.DTOs;
using ChatApp.Domain;

namespace ChatApp.API.Mapping;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {
        CreateMap<User, SignInDto>().ReverseMap();
        CreateMap<User, SignUpDto>().ReverseMap();
        CreateMap<User, UserDto>();
    }
}