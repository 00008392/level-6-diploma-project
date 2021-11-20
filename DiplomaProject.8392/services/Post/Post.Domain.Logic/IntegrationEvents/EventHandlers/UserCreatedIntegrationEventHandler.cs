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
using AutoMapper;

namespace Post.Domain.Logic.IntegrationEvents.EventHandlers
{
    //tested
    public class UserCreatedIntegrationEventHandler: BaseIntegrationEventHandler, IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        public UserCreatedIntegrationEventHandler(IEventHandlerService service, IMapper mapper)
            :base(service, mapper)
        {
        }

        public async Task Handle(UserCreatedIntegrationEvent @event)
        {
            var user = _mapper.Map<CreateUserDTO>(@event);
            await _service.CreateUserAsync(user);
        }
    }
}
