namespace ProducerService.Application.Dto;

public record NewEventResponseDto(
    int Id,
    string ProducerId,
    string Name,
    string Description,
    string Location,
    DateTime StartTime,
    DateTime EndTime);