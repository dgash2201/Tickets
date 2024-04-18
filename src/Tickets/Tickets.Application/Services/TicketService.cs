using Common.Application.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Tickets.Application.Dto;
using Tickets.Application.Interfaces;
using Tickets.Domain.Entities;

namespace Tickets.Application.Services;

public class TicketService : ITicketService
{
    private readonly IApplicationContext _context;

    public TicketService(IApplicationContext context) => _context = context;

    public async ValueTask<Guid?> CreateAsync(Guid eventId, string title, string? description, decimal price,
        int maxCount, DateTime salesStartDate, DateTime salesEndDate, CancellationToken cancellationToken = default)
    {
        if (!await IsUniqueName(eventId, title, cancellationToken))
            return null;

        var ticketTypeId = Guid.NewGuid();
        var ticketType = new TicketType
        {
            Id = ticketTypeId,
            EventId = eventId,
            Title = title,
            Description = description,
            Price = price,
            CurrentCount = 0,
            MaxCount = maxCount,
            SalesStartDate = salesStartDate,
            SalesEndDate = salesEndDate
        };

        await _context.TicketTypes.AddAsync(ticketType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ticketTypeId;
    }

    public async ValueTask<bool> BookAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var ticketType = await _context.TicketTypes.SingleOrDefaultAsync(item => item.Id == id,
            cancellationToken: cancellationToken);

        if (ticketType is null || ticketType.CurrentCount >= ticketType.MaxCount)
            return false;

        ticketType.CurrentCount++;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async ValueTask<TicketTypeDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.TicketTypes.SingleOrDefaultAsync(item => item.Id == id,
                         cancellationToken: cancellationToken) ??
                     throw new EntityNotFoundException("Тип билета не найден.");
        var dto = entity.Adapt<TicketTypeDto>();

        return dto;
    }

    public async ValueTask<IReadOnlyCollection<TicketTypeDto>> GetByEventAsync(Guid eventId,
        CancellationToken cancellationToken = default) => (await _context.TicketTypes
            .Where(entity => entity.EventId == eventId)
            .ToArrayAsync(cancellationToken: cancellationToken))
        .Select(entity => entity.Adapt<TicketTypeDto>())
        .ToArray();

    private async ValueTask<bool> IsUniqueName(Guid eventId, string title,
        CancellationToken cancellationToken = default) => !await _context.TicketTypes
        .Where(entity => entity.EventId == eventId)
        .AnyAsync(entity => entity.Title == title, cancellationToken: cancellationToken);
}
