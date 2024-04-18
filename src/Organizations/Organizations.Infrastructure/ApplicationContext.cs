using Microsoft.EntityFrameworkCore;
using Organizations.Application;
using Organizations.Domain.Entities;

namespace Organizations.Infrastructure;

public class ApplicationContext : DbContext, IApplicationContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Organization> Organizations { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().UseTptMappingStrategy();
        
        base.OnModelCreating(modelBuilder);
    }
}