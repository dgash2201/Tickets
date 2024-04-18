using Organizations.Application.Dto;

namespace Organizations.Application.Interfaces;

public interface IOrganizationService
{
    Task<OrganizationDto?> GetById(Guid organizationId);

    Task<Guid> Register(string login, string name, string password, string inn, string ogrn);
}