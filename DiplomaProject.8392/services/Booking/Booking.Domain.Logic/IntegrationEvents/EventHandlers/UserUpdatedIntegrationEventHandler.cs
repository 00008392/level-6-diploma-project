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
    public class UserUpdatedIntegrationEventHandler : BaseIntegrationEventHandler<User>,
        IIntegrationEventHandler<UserUpdatedIntegrationEvent>
    {
        private readonly IMapper _mapper;
        public UserUpdatedIntegrationEventHandler(IEventHandlerService<User> service,
            IMapper mapper)
            : base(service)
        {
            _mapper = mapper;
        }

        public async Task Handle(UserUpdatedIntegrationEvent @event)
        {
            var userDTO = _mapper.Map<UserDTO>(@event);
            await _service.UpdateEntityAsync(userDTO);
        }
    }
}
