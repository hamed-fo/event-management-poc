namespace SubscriptionService.UserInterface.Dto;

public record NewRegistrationRequestDto(
    int EventId,
    string Name,
    string PhoneNumber,
    string Email);