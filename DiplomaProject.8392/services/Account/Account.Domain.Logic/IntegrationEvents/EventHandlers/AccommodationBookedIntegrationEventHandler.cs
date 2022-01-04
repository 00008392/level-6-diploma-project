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
    public class AccommodationBookedIntegrationEventHandler : BaseIntegrationEventHandler,
        IIntegrationEventHandler<AccommodationBookedIntegrationEvent>
    {
        private readonly IMapper _mapper;
        public AccommodationBookedIntegrationEventHandler(
            IEventHandlerService service,
            IMapper mapper) : base(service)
        {
            _mapper = mapper;
        }

        public async Task Handle(AccommodationBookedIntegrationEvent @event)
        {
            var booking = _mapper.Map<AddBookingDTO>(@event);
            await _service.CreateBooking(booking);
        }
    }
}
