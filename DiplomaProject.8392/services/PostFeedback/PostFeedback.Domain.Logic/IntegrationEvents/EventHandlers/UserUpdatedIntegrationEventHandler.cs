﻿using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
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
    //event handler that reacts when user information is updated
    public class UserUpdatedIntegrationEventHandler: BaseIntegrationEventHandler, IIntegrationEventHandler<UserUpdatedIntegrationEvent>
    {
        public UserUpdatedIntegrationEventHandler(
            IEventHandlerService service,
            IMapper mapper)
            :base(
                 service,
                 mapper)
        {
        }
        public async Task Handle(UserUpdatedIntegrationEvent @event)
        {
            var userDTO = _mapper.Map<UserDTO>(@event);
            //call service to update user in this microservice
            await _service.UpdateUserAsync(userDTO);
        }

    }
}
