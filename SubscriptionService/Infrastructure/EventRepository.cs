using Microsoft.EntityFrameworkCore;
using SubscriptionService.Domain.Entity;
using SubscriptionService.Domain.Repository;

namespace SubscriptionService.Infrastructure;

public class EventRepository: IEventRepository
{
    private readonly RegistrationDbContext _dbContext;

    public EventRepository(RegistrationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Event>> GetAllAsync()
    {
        return await _dbContext.Events.ToListAsync();
    }
    
    public async Task SaveAsync(Event newEvent)
    {
        _dbContext.Events.Add(newEvent);
        await _dbContext.SaveChangesAsync();
    }
}