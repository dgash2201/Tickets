namespace Tickets.Api.Ports;

public interface IPaymentGrpcClient
{
    ValueTask<int> GetPaymentsCountAsync(Guid ticketTypeId, CancellationToken cancellationToken = default);
}
