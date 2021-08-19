
using Account.Domain.Entities;
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Account.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.IntegrationEvents.EventHandlers
{
   public class UserDeletedIntegrationEventHandler: BaseIntegrationEventHandler
    {
        public UserDeletedIntegrationEventHandler(IEventHandlerService service)
            :base(service)
        {
        }
        public async Task Handle(UserDeletedIntegrationEvent @event)
        {
            await _service.DeleteUserAsync(@event.UserId);
        }
    }
}
