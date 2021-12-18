using AutoMapper;
using EventBus.Contracts;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Post.Domain.Logic.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.IntegrationEvents.EventHandlers
{
    public class AccommodationBookedIntegrationEventHandler : BaseIntegrationEventHandler,
        IIntegrationEventHandler<AccommodationBookedIntegrationEvent>
    {
        public AccommodationBookedIntegrationEventHandler(
            IEventHandlerService service,
            IMapper mapper) : base(service, mapper)
        {
        }

        public async Task Handle(AccommodationBookedIntegrationEvent @event)
        {
            var booking = _mapper.Map<AddBookingDTO>(@event);
            await _service.AddBookingAsync(booking);
        }
    }
}
