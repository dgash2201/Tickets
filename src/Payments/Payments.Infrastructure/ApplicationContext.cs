using Microsoft.EntityFrameworkCore;
using Payments.Application;
using Payments.Domain.Entities;

namespace Payments.Infrastructure;

public class ApplicationContext : DbContext, IApplicationContext
{
    public DbSet<Payment> Payments { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
}
