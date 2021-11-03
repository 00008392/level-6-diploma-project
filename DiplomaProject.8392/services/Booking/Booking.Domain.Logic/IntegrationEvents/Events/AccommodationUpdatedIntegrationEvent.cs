
using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.IntegrationEvents.Events
{
    //tested
    public class AccommodationUpdatedIntegrationEvent: IntegrationEvent
    {
        public long AccommodationId { get;private set; }
        public string Title { get; protected set; }
        public long OwnerId { get; protected set; }
        public string Address { get; protected set; }
        public string ContactNumber { get; protected set; }
        public int? RoomsNo { get; protected set; }
        public int? BathroomsNo { get; protected set; }
        public int? BedsNo { get; protected set; }
        public int MaxGuestsNo { get; protected set; }
        public int? SquareMeters { get; protected set; }
        public decimal Price { get; protected set; }
        public bool? IsWholeApartment { get; protected set; }
        public string MovingInTime { get; protected set; }
        public string MovingOutTime { get; protected set; }

        public AccommodationUpdatedIntegrationEvent(
            long accommodationId, string title, long ownerId, string address, string contactNumber, int? roomsNo,
            int? bathroomsNo, int? bedsNo, int maxGuestsNo, int? squareMeters, decimal price, bool? isWholeApartment,
            string movingInTime, string movingOutTime)
        {
            AccommodationId = accommodationId;
            Title = title;
            OwnerId = ownerId;
            Address = address;
            ContactNumber = contactNumber;
            RoomsNo = roomsNo;
            BathroomsNo = bathroomsNo;
            BedsNo = bedsNo;
            MaxGuestsNo = maxGuestsNo;
            SquareMeters = squareMeters;
            Price = price;
            IsWholeApartment = isWholeApartment;
            MovingInTime = movingInTime;
            MovingOutTime = movingOutTime;
        }
    }
}
