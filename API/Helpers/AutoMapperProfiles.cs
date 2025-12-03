using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, UserDto>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country == null ? string.Empty : src.Country.Name))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department == null ? string.Empty : src.Department.Name))
            .ForMember(dest => dest.Municipality, opt => opt.MapFrom(src => src.Municipality == null ? string.Empty : src.Municipality.Name));

        CreateMap<CreateUserDto, AppUser>();
    }
}