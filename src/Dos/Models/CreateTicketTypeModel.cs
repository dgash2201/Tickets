namespace Dos.Models;

public record CreateTicketTypeModel
{
    public Guid EventId { get; init; }

    public required string Title { get; init; }

    public string? Description { get; init; }

    public decimal Price { get; init; }

    public int MaxCount { get; init; }

    public DateTime SalesStartDate { get; init; }

    public DateTime SalesEndDate { get; init; }
}
