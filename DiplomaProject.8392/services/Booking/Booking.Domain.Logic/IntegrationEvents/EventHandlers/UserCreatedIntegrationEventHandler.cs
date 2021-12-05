using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Booking.Domain.Entities;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Booking.Domain.Logic.IntegrationEvents.Events;
using EventBus.Contracts;

namespace Booking.Domain.Logic.IntegrationEvents.EventHandlers
{
    //tested
    public class UserCreatedIntegrationEventHandler : BaseIntegrationEventHandler<User>,
        IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IMapper _mapper;
        public UserCreatedIntegrationEventHandler(IEventHandlerService<User> service, IMapper mapper) 
            : base(service)
        {
            _mapper = mapper;
        }

        public async Task Handle(UserCreatedIntegrationEvent @event)
        {
            var userDTO = _mapper.Map<UserDTO>(@event);
            await _service.CreateEntityAsync(userDTO);
        }
    }
}
