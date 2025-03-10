namespace SubscriptionService.Application.Dto;

public record NewRegistrationResponseDto(
    int Id,
    int EventId,
    string Name,
    string PhoneNumber,
    string Email);