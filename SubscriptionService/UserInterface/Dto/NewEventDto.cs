namespace SubscriptionService.UserInterface.Dto;

public record NewEventDto(
    int Id,
    string Name,
    string Description,
    string Location,
    DateTime StartTime,
    DateTime EndTime);