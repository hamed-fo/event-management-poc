using ProducerService.Application.Dto;
using ProducerService.Controller.Dto;
using ProducerService.Domain.Entity;
using ProducerService.Domain.Service;

namespace ProducerService.Application;

public class RegistrationUseCase
{
    private readonly RegistrationService _registrationService;

    public RegistrationUseCase(RegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    public async Task<List<RegistrationsResponseDto>> GetRegistrationsAsync(int eventId)
    {
        var registrations = await _registrationService.GetRegistrationsAsync(eventId);

        return registrations.Select(r => new RegistrationsResponseDto(
            r.Id,
            r.EventId,
            r.Name,
            r.PhoneNumber,
            r.Email)).ToList();
    }

    public async Task SaveRegistrationAsync(NewRegistrationDto registrationDto)
    {
        var registration = new Registration
        {
            Id = registrationDto.Id,
            EventId = registrationDto.EventId,
            Name = registrationDto.Name,
            PhoneNumber = registrationDto.PhoneNumber,
            Email = registrationDto.Email
        };
        
        await _registrationService.SaveRegistrationAsync(registration);
    }
}