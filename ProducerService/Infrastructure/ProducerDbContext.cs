using Microsoft.EntityFrameworkCore;
using ProducerService.Domain.Entity;

namespace ProducerService.Infrastructure;

public class ProducerDbContext: DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    
    public ProducerDbContext(DbContextOptions<ProducerDbContext> options): base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>().HasKey(e => e.Id);
        modelBuilder.Entity<Registration>().HasKey(r => r.Id);
        base.OnModelCreating(modelBuilder);
    }
}