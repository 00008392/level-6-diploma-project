﻿using AutoMapper;
using EventBus.Contracts;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using PostFeedback.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers
{
    //event handler that reacts when booking request is accepted
    public class BookingAcceptedIntegrationEventHandler : BaseIntegrationEventHandler,
        IIntegrationEventHandler<BookingAcceptedIntegrationEvent>
    {
        public BookingAcceptedIntegrationEventHandler(
            IEventHandlerService service,
            IMapper mapper) : base(
                service,
                mapper)
        {
        }

        public async Task Handle(BookingAcceptedIntegrationEvent @event)
        {
            var booking = _mapper.Map<AddBookingDTO>(@event);
            //call necessary service to create booking in this microservice
            await _service.AddBookingAsync(booking);
        }
    }
}
