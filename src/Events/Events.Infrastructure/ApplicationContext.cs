using Events.Application;
using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Event> Events { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
    }
}
