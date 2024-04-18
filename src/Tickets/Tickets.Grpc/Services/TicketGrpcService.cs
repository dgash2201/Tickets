using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Tickets.Application.Interfaces;

namespace Tickets.Grpc.Services;

public class TicketGrpcService : Tickets.TicketsBase
{
    private readonly ITicketService _ticketService;

    public TicketGrpcService(ITicketService ticketService) => _ticketService = ticketService;

    public override async Task<GetTicketTypeResponse> GetTicketType(GetTicketTypeRequest request,
        ServerCallContext context)
    {
        var ticketTypeId = Guid.Parse(request.Id);

        var ticketType = await _ticketService.GetByIdAsync(ticketTypeId);
        var response = new GetTicketTypeResponse
        {
            Id = ticketType.Id.ToString(),
            EventId = ticketType.EventId.ToString(),
            Title = ticketType.Title,
            Description = ticketType.Description,
            MaxCount = ticketType.MaxCount,
            SalesStartDate = ticketType.SalesStartDate.ToTimestamp(),
            SalesEndDate = ticketType.SalesEndDate.ToTimestamp()
        };

        return response;
    }

    public override async Task<BookResponse> Book(BookRequest request, ServerCallContext context)
    {
        var ticketTypeId = Guid.Parse(request.Id);

        var success = await _ticketService.BookAsync(ticketTypeId);
        var response = new BookResponse { Success = success };

        return response;
    }
}
