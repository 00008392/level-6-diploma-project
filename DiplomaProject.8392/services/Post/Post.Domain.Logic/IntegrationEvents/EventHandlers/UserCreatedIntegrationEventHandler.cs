using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
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
    public class UserCreatedIntegrationEventHandler: BaseIntegrationEventHandler, IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        public UserCreatedIntegrationEventHandler(IEventHandlerService service)
            :base(service)
        {

        }

        public async Task Handle(UserCreatedIntegrationEvent @event)
        {
            var user = new CreateUserDTO
            {
                Email = @event.Email
            };
            await _service.CreateUserAsync(user);
        }
    }
}
