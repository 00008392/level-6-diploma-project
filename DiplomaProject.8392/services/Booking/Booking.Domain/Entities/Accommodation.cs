﻿using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
   public class Accommodation: BaseEntity
    {
        public string Title { get; private set; }
        public long OwnerId { get; private set; }
        public User Owner { get; }
        public string Address { get; private set; }
        public string ContactNumber { get; private set; }
        public int? RoomsNo { get; private set; }
        public int? BathroomsNo { get; private set; }
        public int? BedsNo { get; private set; }
        public int MaxGuestsNo { get; private set; }
        public int? SquareMeters { get; private set; }
        public decimal Price { get; private set; }
        public bool? IsWholeApartment { get; private set; }
        public string MovingInTime { get; private set; }
        public string MovingOutTime { get; private set; }
        public ICollection<BookingRequest> BookingRequests { get; }

        public Accommodation(string title, long ownerId,
            string address,
            string contactNumber, int? roomsNo, 
            int? bathroomsNo, int? bedsNo, 
            int maxGuestsNo, int? squareMeters, 
            decimal price,  bool? isWholeApartment,
            string movingInTime, string movingOutTime)
        {
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
        public Accommodation(long id, string title, long ownerId,
           string address,
           string contactNumber, int? roomsNo,
           int? bathroomsNo, int? bedsNo,
           int maxGuestsNo, int? squareMeters,
           decimal price,  bool? isWholeApartment,
           string movingInTime, string movingOutTime):base(id)
        {
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
