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
    class UserUpdatedIntegrationEventHandler : BaseIntegrationEventHandler<User>,
        IIntegrationEventHandler<UserUpdatedIntegrationEvent>
    {
        public UserUpdatedIntegrationEventHandler(IEventHandlerService<User> service)
            : base(service)
        {
        }

        public async Task Handle(UserUpdatedIntegrationEvent @event)
        {
            var userBase = new UserDTO(@event.FirstName, @event.LastName, @event.Email,
                @event.PhoneNumber, @event.Address, @event.DateOfBirth);
            var userDTO = new UpdateUserDTO(@event.UserId, userBase);
            await _service.UpdateEntity(userDTO);
        }
    }
}
