using SubscriptionService.Application.Dto;
using SubscriptionService.Domain.Entity;
using SubscriptionService.Domain.Service;
using SubscriptionService.UserInterface.Dto;

namespace SubscriptionService.Application;

public class RegistrationUseCase
{
    private readonly RegistrationService _registrationService;

    public RegistrationUseCase(RegistrationService registrationService)
    {
        _registrationService = registrationService;
    }
    
    public async Task<NewRegistrationResponseDto> NewRegistrationAsync(NewRegistrationRequestDto newRegistrationRequest)
    {
        var registration = new Registration
        {
            EventId = newRegistrationRequest.EventId,
            Name = newRegistrationRequest.Name,
            PhoneNumber = newRegistrationRequest.PhoneNumber,
            Email = newRegistrationRequest.Email
        };
        
        var registrationEntity = await _registrationService.RegisterAsync(registration);

        return new NewRegistrationResponseDto(
            registrationEntity.Id,
            registrationEntity.EventId,
            registrationEntity.Name,
            registrationEntity.PhoneNumber,
            registrationEntity.Email);
    }
}