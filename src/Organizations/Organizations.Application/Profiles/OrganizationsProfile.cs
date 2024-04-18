using AutoMapper;
using Organizations.Application.Dto;
using Organizations.Domain.Entities;

namespace Organizations.Application.Profiles;

public class OrganizationsProfile : Profile
{
    public OrganizationsProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<Organization, OrganizationDto>()
            .IncludeBase<User, UserDto>();
    }
}