using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Entities;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.IntegrationEvents.EventHandlers.Core;
using Booking.Domain.Logic.IntegrationEvents.Events;
using EventBus.Contracts;

namespace Booking.Domain.Logic.IntegrationEvents.EventHandlers
{
    class AccommodationUpdatedIntegrationEventHandler : BaseIntegrationEventHandler<Accommodation>,
        IIntegrationEventHandler<AccommodationUpdatedIntegrationEvent>
    {
        public AccommodationUpdatedIntegrationEventHandler(IEventHandlerService<Accommodation> service)
            : base(service)
        {
        }

        public async Task Handle(AccommodationUpdatedIntegrationEvent @event)
        {
            var baseAccommodation = new BaseAccommodationDTO(@event.Title,
                @event.OwnerId,  @event.Address, @event.ContactNumber,
                @event.RoomsNo, @event.BathroomsNo, @event.BedsNo, @event.MaxGuestsNo,
                @event.SquareMeters, @event.Price, @event.IsWholeApartment, @event.MovingInTime,
                @event.MovingOutTime);
            var accommodationDTO = new UpdateAccommodationDTO(@event.AccommodationId,
                baseAccommodation);
            await _service.UpdateEntity(accommodationDTO);
        }
    }
}
