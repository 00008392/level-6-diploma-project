using Profile.Domain.Logic.Contracts;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.IntegrationEvents.EventHandlers
{
    public class UserCreatedIntegrationEventHandler
    {
        private readonly IEventHandlerService _service;
        public UserCreatedIntegrationEventHandler(IEventHandlerService service)
        {
            _service = service;
        }
        public async Task Handle(UserCreatedIntegrationEvent @event)
        {
            var user = new CreateProfileDTO
            {
                Email = @event.Email,
                RegistrationDate = @event.RegistrationDate
            };
            await _service.CreateUserAsync(user);
        }  
    }
}
