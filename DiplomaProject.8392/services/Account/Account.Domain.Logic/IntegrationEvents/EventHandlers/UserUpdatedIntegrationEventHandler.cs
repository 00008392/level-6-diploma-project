using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Account.Domain.Logic.IntegrationEvents.Events;
using EventBus.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.EventHandlers
{
    //tested
   public class UserUpdatedIntegrationEventHandler: BaseIntegrationEventHandler,
        IIntegrationEventHandler<UserUpdatedIntegrationEvent>
    {
        public UserUpdatedIntegrationEventHandler(IEventHandlerService service)
            :base(service)
        {
        }
        public async Task Handle(UserUpdatedIntegrationEvent @event)
        {
            var userDTO = new UpdateUserDTO(@event.UserId, @event.Email);
            await _service.UpdateUserAsync(userDTO);
        }
    }
}
