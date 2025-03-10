namespace ProducerService.Controller.Dto;

public record NewEventRequestDto(
    string Name,
    string Description,
    string Location,
    DateTime StartTime,
    DateTime EndTime);