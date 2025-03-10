using ProducerService.Domain.Entity;
using ProducerService.Domain.Repository;
using ProducerService.Infrastructure.Mapper;

namespace ProducerService.Infrastructure;

public class EventRepository: IEventRepository
{
    private readonly ProducerDbContext _dbContext;
    private readonly RabbitMqPublisher _rabbitMqPublisher;
    private readonly EventMapper _eventMapper;
    
    public EventRepository(ProducerDbContext dbContext, RabbitMqPublisher rabbitMqPublisher, EventMapper eventMapper)
    {
        _dbContext = dbContext;
        _rabbitMqPublisher = rabbitMqPublisher;
        _eventMapper = eventMapper;
    }

    public async Task<Event> saveAsync(Event eventEntity)
    {
        _dbContext.Set<Event>().Add(eventEntity);
        await _dbContext.SaveChangesAsync();
        await _rabbitMqPublisher.PublishEventAsync(_eventMapper.toJson(eventEntity));
        return eventEntity;
    }
}