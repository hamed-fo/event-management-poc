using ProducerService.Domain.Entity;

namespace ProducerService.Infrastructure.Mapper;

public class EventMapper
{
    public string toJson(Event eventEntity)
    {
        return System.Text.Json.JsonSerializer.Serialize(eventEntity);
    }
}