using Microsoft.EntityFrameworkCore;
using ProducerService.Domain.Entity;
using ProducerService.Domain.Repository;

namespace ProducerService.Infrastructure;

public class RegistrationRepository: IRegistrationRepository
{
    private ProducerDbContext _dbContext;

    public RegistrationRepository(ProducerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Registration>> GetRegistrationsAsync(int eventId)
    {
        return await _dbContext.Registrations.Where(r => r.EventId == eventId).ToListAsync();
    }

    public async Task SaveRegistrationAsync(Registration registration)
    {
        _dbContext.Registrations.Add(registration);
        await _dbContext.SaveChangesAsync();
    }
}