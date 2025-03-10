namespace SubscriptionService.Application.Dto;

public record EventsResponseDto(
    int Id,
    string Name,
    string Description,
    string Location,
    DateTime StartTime,
    DateTime EndTime);