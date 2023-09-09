using AutoMapper;
using TheSocial_Auth.Models.Dtos;
using TheSocial_Auth.Models;

namespace TheSocial_Auth.Profiles
{
    public class AuthProfiles : Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegisterUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, u => u.MapFrom(reg => reg.Email));

            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}
