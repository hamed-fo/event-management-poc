using SubscriptionService.Domain.Entity;

namespace SubscriptionService.Infrastructure.Mapper;

public class RegistrationMapper
{
    public string toJson(Registration registrationEntity)
    {
        return System.Text.Json.JsonSerializer.Serialize(registrationEntity);
    }
}