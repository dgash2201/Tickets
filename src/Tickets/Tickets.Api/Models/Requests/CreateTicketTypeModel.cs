using System.ComponentModel.DataAnnotations;

namespace Tickets.Api.Models.Requests;

public record CreateTicketTypeModel
{
    public Guid EventId { get; init; }

    [Required]
    public required string Title { get; init; }

    public string? Description { get; init; }

    [Range(0.01, int.MaxValue)]
    public decimal Price { get; init; }

    [Range(1, int.MaxValue)]
    public int MaxCount { get; init; }

    public DateTime SalesStartDate { get; init; }

    public DateTime SalesEndDate { get; init; }
}
