using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Events.Application
{
    public interface IApplicationContext
    {
        DbSet<Event> Events { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}