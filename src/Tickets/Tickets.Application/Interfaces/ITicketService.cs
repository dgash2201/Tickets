using Tickets.Application.Dto;

namespace Tickets.Application.Interfaces;

public interface ITicketService
{
    ValueTask<Guid?> CreateAsync(Guid eventId, string title, string? description, decimal price, int maxCount,
        DateTime salesStartDate, DateTime salesEndDate, CancellationToken cancellationToken = default);

    ValueTask<bool> BookAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<TicketTypeDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<IReadOnlyCollection<TicketTypeDto>> GetByEventAsync(Guid eventId,
        CancellationToken cancellationToken = default);
}
