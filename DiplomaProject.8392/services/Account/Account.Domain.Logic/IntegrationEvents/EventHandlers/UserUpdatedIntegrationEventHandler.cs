using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Account.Domain.Logic.IntegrationEvents.Events;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserUpdatedIntegrationEventHandler(IEventHandlerService service, 
            IMapper mapper)
            :base(service)
        {
            _mapper = mapper;
        }
        public async Task Handle(UserUpdatedIntegrationEvent @event)
        {
            var userDTO = _mapper.Map<UpdateUserDTO>(@event);
            await _service.UpdateUserAsync(userDTO);
        }
    }
}
