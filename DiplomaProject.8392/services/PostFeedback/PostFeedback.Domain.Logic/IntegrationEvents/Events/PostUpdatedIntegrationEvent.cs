using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.IntegrationEvents.Events
{
    //this event is published by post microservice through event bus when post information is updated
    public class PostUpdatedIntegrationEvent: IntegrationEvent
    {
        public long PostId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public long OwnerId { get; private set; }
        public long? CategoryId { get; private set; }
        public long CityId { get; private set; }
        public string Address { get; private set; }
        public string ContactNumber { get; private set; }
        public int? RoomsNo { get; private set; }
        public int? BathroomsNo { get; private set; }
        public int BedsNo { get; private set; }
        public int MaxGuestsNo { get; private set; }
        public int? SquareMeters { get; private set; }
        public decimal Price { get; private set; }
        public bool IsWholeApartment { get; private set; }
        public string MovingInTime { get; private set; }
        public string MovingOutTime { get; private set; }

        public PostUpdatedIntegrationEvent(
            long postId,
            string title,
            string description,
            long ownerId,
            long? categoryId,
            long cityId,
            string address,
            string contactNumber,
            int? roomsNo,
            int? bathroomsNo,
            int bedsNo,
            int maxGuestsNo,
            int? squareMeters,
            decimal price,
            bool isWholeApartment,
            string movingInTime,
            string movingOutTime)
        {
            PostId = postId;
            Title = title;
            Description = description;
            OwnerId = ownerId;
            CategoryId = categoryId;
            CityId = cityId;
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

