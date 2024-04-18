using Microsoft.EntityFrameworkCore;
using Payments.Domain.Entities;

namespace Payments.Application;

public interface IApplicationContext
{
    DbSet<Payment> Payments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
