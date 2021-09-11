using Booking.Domain.Logic.IntegrationEvents.Events.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.Events
{
    public class AccommodationCreatedIntegrationEvent : BaseAccommodationIntegrationEvent
    {
        public AccommodationCreatedIntegrationEvent(string title, long ownerId,
            string address, string contactNumber, 
            int? roomsNo, int? bathroomsNo, int? bedsNo, int maxGuestsNo,
            int? squareMeters, decimal price,
            bool? isWholeApartment, string movingInTime, 
            string movingOutTime) : base(title, ownerId, address,
                contactNumber, roomsNo, bathroomsNo, bedsNo, maxGuestsNo, 
                squareMeters, price, isWholeApartment,
                movingInTime, movingOutTime)
        {
        }
    }
}
