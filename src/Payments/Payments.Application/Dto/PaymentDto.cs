using Payments.Domain.Entities;

namespace Payments.Application.Dto;

public class PaymentDto
{
    public Guid Id { get; set; }

    public Guid TicketTypeId { get; set; }

    public Guid UserId { get; set; }

    public decimal Price { get; init; }

    public PurchaseStatus PurchaseStatus { get; set; }

    public DateTime ChangeDate { get; set; }
}
