using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using PostFeedback.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Contracts;
using AutoMapper;

namespace PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers
{
   public class UserDeletedIntegrationEventHandler: BaseIntegrationEventHandler, IIntegrationEventHandler<UserDeletedIntegrationEvent>
    {
        //tested
        public UserDeletedIntegrationEventHandler(IEventHandlerService service, IMapper mapper)
            :base(service, mapper)
        {

        }
        public async Task Handle(UserDeletedIntegrationEvent @event)
        {
            await _service.DeleteUserAsync(@event.UserId);
        }
    }
}
