using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Entities;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Booking.Domain.Logic.IntegrationEvents.Events;
using EventBus.Contracts;

namespace Booking.Domain.Logic.IntegrationEvents.EventHandlers
{
    public class UserCreatedIntegrationEventHandler : BaseIntegrationEventHandler<User>,
        IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        public UserCreatedIntegrationEventHandler(IEventHandlerService<User> service) 
            : base(service)
        {
        }

        public async Task Handle(UserCreatedIntegrationEvent @event)
        {
            var userDTO = new CreateUserDTO(@event.Email);
            await _service.CreateEntityAsync(userDTO);
        }
    }
}
