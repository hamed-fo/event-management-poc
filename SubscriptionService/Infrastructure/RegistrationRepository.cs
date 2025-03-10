using SubscriptionService.Domain.Entity;
using SubscriptionService.Domain.Repository;
using SubscriptionService.Infrastructure.Mapper;

namespace SubscriptionService.Infrastructure;

public class RegistrationRepository: IRegistrationRepository
{
    private readonly RegistrationDbContext _dbContext;
    private readonly RabbitMqPublisher _rabbitMqPublisher;
    private readonly RegistrationMapper _mapper;

    public RegistrationRepository(
        RegistrationDbContext dbContext,
        RabbitMqPublisher rabbitMqPublisher,
        RegistrationMapper mapper)
    {
        _dbContext = dbContext;
        _rabbitMqPublisher = rabbitMqPublisher;
        _mapper = mapper;
    }

    public async Task<Registration> SaveAsync(Registration registration)
    {
        _dbContext.Registrations.Add(registration);
        await _dbContext.SaveChangesAsync();

        var registrationJson = _mapper.toJson(registration);
        await _rabbitMqPublisher.PublishRegistrationAsync(registrationJson);

        return registration;
    }
}