﻿using EventBus.Events;
using Post.Domain.Logic.IntegrationEvents.Events.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.IntegrationEvents.Events
{
    //tested
    public class AccommodationCreatedIntegrationEvent: IntegrationEvent
    {
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public long OwnerId { get; protected set; }
        public long? CategoryId { get; protected set; }
        public string Address { get; protected set; }
        public string ReferencePoint { get; protected set; }
        public string ContactNumber { get; protected set; }
        public int? RoomsNo { get; protected set; }
        public int? BathroomsNo { get; protected set; }
        public int? BedsNo { get; protected set; }
        public int MaxGuestsNo { get; protected set; }
        public int? SquareMeters { get; protected set; }
        public decimal Price { get; protected set; }
        public decimal? Latitude { get; protected set; }
        public decimal? Longitude { get; protected set; }
        public bool? IsWholeApartment { get; protected set; }
        public string MovingInTime { get; protected set; }
        public string MovingOutTime { get; protected set; }
        public string AdditionalInfo { get; protected set; }
        public DateTime DatePublished { get; }

        public AccommodationCreatedIntegrationEvent(
            string title, string description, long ownerId, long? categoryId, string address, string referencePoint,
            string contactNumber, int? roomsNo, int? bathroomsNo, int? bedsNo, int maxGuestsNo, int? squareMeters,
            decimal price, decimal? latitude, decimal? longitude, bool? isWholeApartment, string movingInTime,
            string movingOutTime, string additionalInfo, DateTime datePublished)
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
            DatePublished = datePublished;
        }
    }
}

