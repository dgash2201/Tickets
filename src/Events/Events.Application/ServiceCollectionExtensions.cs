using Events.Application.Interfaces;
using Events.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Events.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddScoped<IEventService, EventService>();
        }
    }
}