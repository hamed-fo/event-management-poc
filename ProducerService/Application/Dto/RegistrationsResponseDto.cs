namespace ProducerService.Application.Dto;

public record RegistrationsResponseDto(
    int Id,
    int EventId,
    string Name,
    string PhoneNumber,
    string Email);