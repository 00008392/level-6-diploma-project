using Post.Domain.Logic.IntegrationEvents.Events.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.IntegrationEvents.Events
{
    public class AccommodationUpdatedIntegrationEvent: AccommodationBaseIntegrationEvent
    {
        public long AccommodationId { get; }

        public AccommodationUpdatedIntegrationEvent(string title, string description,
          long ownerId, long? categoryId,
          string address, string referencePoint,
          string contactNumber, int? roomsNo,
          int? bathroomsNo, int? bedsNo,
          int maxGuestsNo, int? squareMeters,
          decimal price, decimal? latitude,
          decimal? longitude, bool? isWholeApartment,
          string movingInTime, string movingOutTime,
          string additionalInfo, long accommodationId)
        {
            Title = title;
            Description = description;
            OwnerId = ownerId;
            CategoryId = categoryId;
            Address = address;
            ReferencePoint = referencePoint;
            ContactNumber = contactNumber;
            RoomsNo = roomsNo;
            BathroomsNo = bathroomsNo;
            BedsNo = bedsNo;
            MaxGuestsNo = maxGuestsNo;
            SquareMeters = squareMeters;
            Price = price;
            Latitude = latitude;
            Longitude = longitude;
            IsWholeApartment = isWholeApartment;
            MovingInTime = movingInTime;
            MovingOutTime = movingOutTime;
            AdditionalInfo = additionalInfo;
            AccommodationId = accommodationId;
        }
    }
}

