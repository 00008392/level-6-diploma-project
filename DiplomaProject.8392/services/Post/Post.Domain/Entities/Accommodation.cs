using BaseClasses.Entities;
using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class Accommodation: FeedbackEntity
    {

        public string Title { get; private set; } //search
        public string Description { get; private set; } //search
        public long OwnerId { get; private set; } //filter
        public User Owner { get; }
        public DateTime DatePublished { get; private set; } 
        public long? CategoryId { get; private set; } //filter
        public Category Category { get; }
        public string Address { get; private set; }
        public long? CityId { get; private set; } //filter
        public City City { get; private set; }
        public string ReferencePoint { get; private set; }
        public string ContactNumber { get; private set; }
        public int? RoomsNo { get; private set; } //filter
        public int? BathroomsNo { get; private set; }
        public int? BedsNo { get; private set; } //filter
        public int MaxGuestsNo { get; private set; } //filter
        public int? SquareMeters { get; private set; } 
        public decimal Price { get; private set; } //filter 
        public decimal? Latitude { get; private set; }
        public decimal? Longitude { get; private set; }
        public bool? IsWholeApartment { get; private set; } //filter
        public string MovingInTime { get; private set; }
        public string MovingOutTime { get; private set; }
        public string AdditionalInfo { get; private set; }
        public ICollection<AccommodationPhoto> AccommodationPhotos { get;  }
        public ICollection<AccommodationSpecificity> AccommodationSpecificities { get; }
        public ICollection<AccommodationRule> AccommodationRules { get; }
        public ICollection<AccommodationFacility> AccommodationFacilities { get; }
        public ICollection<Booking> Bookings { get; }
        public ICollection<Feedback<Accommodation>> Feedbacks { get; }

        public Accommodation(string title, string description, 
            long ownerId, DateTime datePublished, 
            long? categoryId, long? cityId, string address, 
            string referencePoint, string contactNumber,
            int? roomsNo, int? bathroomsNo, 
            int? bedsNo, int maxGuestsNo, 
            int? squareMeters, decimal price, 
            decimal? latitude, decimal? longitude, 
            bool? isWholeApartment, string movingInTime, 
            string movingOutTime, string additionalInfo)
        {
            Title = title;
            Description = description;
            OwnerId = ownerId;
            DatePublished = datePublished;
            CategoryId = categoryId;
            CityId = cityId;
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
        }
       
        public void UpdateInfo(string title, string description,
            long ownerId, 
            long? categoryId, long? cityId, string address,
            string referencePoint, string contactNumber,
            int? roomsNo, int? bathroomsNo,
            int? bedsNo, int maxGuestsNo,
            int? squareMeters, decimal price,
            decimal? latitude, decimal? longitude,
            bool? isWholeApartment, string movingInTime,
            string movingOutTime, string additionalInfo)
        {
            Title = title;
            Description = description;
            OwnerId = ownerId;
            CategoryId = categoryId;
            CityId = cityId;
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
        }
    }


}
