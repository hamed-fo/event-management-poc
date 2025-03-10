using Microsoft.EntityFrameworkCore;
using SubscriptionService.Domain.Entity;

namespace SubscriptionService.Infrastructure;

public class RegistrationDbContext: DbContext
{
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Event> Events { get; set; }
    
    public RegistrationDbContext(DbContextOptions<RegistrationDbContext> options): base(options) { }
}