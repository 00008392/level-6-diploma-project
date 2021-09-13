using Booking.Domain.Entities;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Booking.Domain.Logic.IntegrationEvents.Events;
using EventBus.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Booking.Domain.Logic.IntegrationEvents.EventHandlers
{
   public class AccommodationCreatedIntegrationEventHandler : BaseIntegrationEventHandler<Accommodation>,
        IIntegrationEventHandler<AccommodationCreatedIntegrationEvent>
    {
        public AccommodationCreatedIntegrationEventHandler(IEventHandlerService<Accommodation> service)
            : base(service)
        {
        }

        public async Task Handle(AccommodationCreatedIntegrationEvent @event)
        {
            var baseAccommodation = new BaseAccommodationDTO(@event.Title,
                @event.OwnerId, @event.Address, @event.ContactNumber,
                @event.RoomsNo, @event.BathroomsNo, @event.BedsNo, @event.MaxGuestsNo,
                @event.SquareMeters, @event.Price, @event.IsWholeApartment, @event.MovingInTime,
                @event.MovingOutTime);
            var accommodationDTO = new CreateAccommodationDTO(baseAccommodation);
            await _service.CreateEntityAsync(accommodationDTO);
        }
    }
}
