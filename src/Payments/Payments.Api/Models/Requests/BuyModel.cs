namespace Payments.Api.Models.Requests;

public record BuyModel
{
    public Guid TicketTypeId { get; init; }

    public Guid UserId { get; init; }
}
