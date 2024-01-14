using AutoMapper;
using DTO.Models;
using Entitles.Models;

namespace DTO.AutoMappers;

public class ApplicationUserMappingProfile : Profile
{
    public ApplicationUserMappingProfile()
    {
        CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();
    }
}