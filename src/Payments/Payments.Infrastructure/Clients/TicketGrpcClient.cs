using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Payments.Application.Dto;
using Payments.Application.Ports;

namespace Payments.Infrastructure.Clients;

public class TicketGrpcClient : ITicketGrpcClient
{
    private readonly Tickets.TicketsClient _ticketsClient;

    public TicketGrpcClient(IConfiguration configuration)
    {
        var channel = GrpcChannel.ForAddress(configuration["TicketsGrpc"]!);
        _ticketsClient = new Tickets.TicketsClient(channel);
    }

    public async ValueTask<TicketTypeDto> GetTicketTypeAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var request = new GetTicketTypeRequest { Id = id.ToString() };
        var response = await _ticketsClient.GetTicketTypeAsync(request, cancellationToken: cancellationToken);

        var ticketTypeDto = new TicketTypeDto
        {
            Id = Guid.Parse(response.Id),
            EventId = Guid.Parse(response.EventId),
            Title = response.Title,
            Description = response.Description,
            MaxCount = response.MaxCount,
            SalesStartDate = response.SalesStartDate.ToDateTime(),
            SalesEndDate = response.SalesEndDate.ToDateTime()
        };

        return ticketTypeDto;
    }

    public async ValueTask<bool> BookAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var request = new BookRequest { Id = id.ToString() };
        var response = await _ticketsClient.BookAsync(request, cancellationToken: cancellationToken);

        return response.Success;
    }
}
