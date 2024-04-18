using Microsoft.EntityFrameworkCore;
using Tickets.Application;
using Tickets.Domain.Entities;

namespace Tickets.Infrastructure;

public class ApplicationContext : DbContext, IApplicationContext
{
    public DbSet<TicketType> TicketTypes { get; set;  }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
}
