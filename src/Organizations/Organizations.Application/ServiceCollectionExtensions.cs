using Microsoft.Extensions.DependencyInjection;
using Organizations.Application.Interfaces;
using Organizations.Application.Profiles;
using Organizations.Application.Services;

namespace Organizations.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IOrganizationService, OrganizationService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<OrganizationService>();
    }

    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        return services
            .AddAutoMapper(typeof(OrganizationsProfile));
    }
}