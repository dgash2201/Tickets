using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;
using Dos.HttpClients;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dos;

public class TrafficSimulator : BackgroundService
{
    private readonly TrafficSimulatorSettings _settings;
    private readonly ILogger<TrafficSimulator> _logger;

    private readonly TicketsClient _ticketsClient;
    private readonly PaymentsClient _paymentsClient;

    public TrafficSimulator(IOptions<TrafficSimulatorSettings> options, TicketsClient ticketsClient,
        PaymentsClient paymentsClient, ILogger<TrafficSimulator> logger)
    {
        _settings = options.Value;

        _ticketsClient = ticketsClient;
        _paymentsClient = paymentsClient;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var ticketTypeId = await _ticketsClient.CreateTicketTypeAsync(_settings.PlacesCount, stoppingToken);

        var channel = Channel.CreateUnbounded<byte>(
            new UnboundedChannelOptions
            {
                SingleWriter = true,
                SingleReader = _settings.ConcurrentTaskCount is 1
            });

        var providerTask = Task.Run(async () => await ProvideAsync(channel, stoppingToken).ConfigureAwait(false),
            stoppingToken);
        var processTasks = Enumerable
            .Range(0, _settings.ConcurrentTaskCount)
            .Select(_ => Task.Run(async () => await ProcessAsync(channel, ticketTypeId, stoppingToken), stoppingToken));

        var tasks = Task.WhenAll(processTasks.Prepend(providerTask));

        await tasks;
    }

    private async Task ProvideAsync(Channel<byte> channel, CancellationToken cancellationToken = default)
    {
        foreach (var _ in Enumerable.Range(0, _settings.RequestMaxCount))
            await channel.Writer.WriteAsync(0, cancellationToken);

        channel.Writer.Complete();
    }

    private async Task ProcessAsync(Channel<byte> channel, Guid ticketTypeId,
        CancellationToken cancellationToken = default)
    {
        await foreach (var _ in channel.Reader.ReadAllAsync(cancellationToken))
            try
            {
                await _paymentsClient.BuyAsync(ticketTypeId, cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error sending purchase request.");
            }
    }
}

public record TrafficSimulatorSettings
{
    [Range(1, int.MaxValue)]
    public int ConcurrentTaskCount { get; set; }

    [Range(1, int.MaxValue)]
    public int PlacesCount { get; set; }

    [Range(1, int.MaxValue)]
    public int RequestMaxCount { get; init; }
}
