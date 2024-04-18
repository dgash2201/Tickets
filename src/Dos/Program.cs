using Dos;
using Dos.HttpClients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient<TicketsClient>()
            .ConfigureHttpClient(httpClient =>
            {
                httpClient.BaseAddress =
                    new Uri(hostContext.Configuration["TicketsHost"] ?? throw new NullReferenceException());
            });
        services.AddHttpClient<PaymentsClient>()
            .ConfigureHttpClient(httpClient =>
            {
                httpClient.BaseAddress =
                    new Uri(hostContext.Configuration["PaymentsHost"] ?? throw new NullReferenceException());
            });

        services.AddHostedService<TrafficSimulator>()
            .AddOptions<TrafficSimulatorSettings>()
            .BindConfiguration(nameof(TrafficSimulator));
    })
    .Build();

await host.RunAsync();
