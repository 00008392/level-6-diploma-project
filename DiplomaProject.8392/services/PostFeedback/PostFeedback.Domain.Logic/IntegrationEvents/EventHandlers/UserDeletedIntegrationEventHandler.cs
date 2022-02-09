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
    //event handler that reacts when user is deleted
    public class UserDeletedIntegrationEventHandler: BaseIntegrationEventHandler, IIntegrationEventHandler<UserDeletedIntegrationEvent>
    {
        public UserDeletedIntegrationEventHandler(
            IEventHandlerService service,
            IMapper mapper)
            :base(
                 service,
                 mapper)
        {

        }
        public async Task Handle(UserDeletedIntegrationEvent @event)
        {
            //call service to delete user from this microservice
            await _service.DeleteUserAsync(@event.UserId);
        }
    }
}
