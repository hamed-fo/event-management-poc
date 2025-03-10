namespace ProducerService.Controller.Dto;

public record NewRegistrationDto(
    int Id,
    int EventId,
    string Name,
    string PhoneNumber,
    string Email);