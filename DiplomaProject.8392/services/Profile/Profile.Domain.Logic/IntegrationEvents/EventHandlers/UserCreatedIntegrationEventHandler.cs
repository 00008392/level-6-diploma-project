using Profile.Domain.Logic.Contracts;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Contracts;

namespace Profile.Domain.Logic.IntegrationEvents.EventHandlers
{
    //tested
    public class UserCreatedIntegrationEventHandler: IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IEventHandlerService _service;
        public UserCreatedIntegrationEventHandler(IEventHandlerService service)
        {
            _service = service;
        }
        public async Task Handle(UserCreatedIntegrationEvent @event)
        {
            var user = new CreateProfileDTO(@event.Email, @event.RegistrationDate);
            await _service.CreateUserAsync(user);
        }  
    }
}
