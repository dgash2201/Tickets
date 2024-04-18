using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payments.Application;
using Payments.Application.Ports;
using Payments.Infrastructure.Clients;

namespace Payments.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        return services
            .AddDbContext<ApplicationContext>(builder => builder
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention())
            .AddScoped<IApplicationContext, ApplicationContext>()
            .AddScoped<ITicketGrpcClient, TicketGrpcClient>();
    }
}
