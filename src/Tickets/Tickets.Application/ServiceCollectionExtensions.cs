using Microsoft.Extensions.DependencyInjection;
using Tickets.Application.Interfaces;
using Tickets.Application.Services;

namespace Tickets.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services) => services
        .AddScoped<ITicketService, TicketService>();
}
