namespace Dos.Models;

public record BuyModel
{
    public Guid TicketTypeId { get; init; }

    public Guid UserId { get; init; }
}
