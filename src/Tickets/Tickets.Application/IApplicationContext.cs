using Microsoft.EntityFrameworkCore;
using Tickets.Domain.Entities;

namespace Tickets.Application;

public interface IApplicationContext
{
    DbSet<TicketType> TicketTypes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
