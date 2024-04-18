using Payments.Application.Dto;

namespace Payments.Application.Ports;

public interface ITicketGrpcClient
{
    ValueTask<TicketTypeDto> GetTicketTypeAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<bool> BookAsync(Guid id, CancellationToken cancellationToken = default);
}
