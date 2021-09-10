using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Post.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Contracts;

namespace Post.Domain.Logic.IntegrationEvents.EventHandlers
{
   public class UserDeletedIntegrationEventHandler: BaseIntegrationEventHandler, IIntegrationEventHandler<UserDeletedIntegrationEvent>
    {
        //tested
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
