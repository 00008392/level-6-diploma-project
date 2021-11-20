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
    public class UserUpdatedIntegrationEventHandler: BaseIntegrationEventHandler, IIntegrationEventHandler<UserUpdatedIntegrationEvent>
    {
        public UserUpdatedIntegrationEventHandler(IEventHandlerService service,
            IMapper mapper)
            :base(service, mapper)
        {
        }
        public async Task Handle(UserUpdatedIntegrationEvent @event)
        {
            var userDTO = _mapper.Map<UpdateUserDTO>(@event);
            await _service.UpdateUserAsync(userDTO);
        }

    }
}
