using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class Accommodation: BaseEntity 
    {

        public string Title { get; private set; }
        public string Description { get; private set; }
        public long OwnerId { get; private set; }
        public Owner Owner { get; }
        public DateTime DatePublished { get; private set; }
        public long? CategoryId { get; private set; }
        public Category Category { get; }
        public string Address { get; private set; }
        public string ReferencePoint { get; private set; }
        public string ContactNumber { get; private set; }
        public int? RoomsNo { get; private set; }
        public int? BathroomsNo { get; private set; }
        public int? BedsNo { get; private set; }
        public int MaxGuestsNo { get; private set; }
        public int? SquareMeters { get; private set; }
        public decimal Price { get; private set; }
        public decimal? Latitude { get; private set; }
        public decimal? Longitude { get; private set; }
        public bool? IsWholeApartment { get; private set; }
        public string MovingInTime { get; private set; }
        public string MovingOutTime { get; private set; }
        public string AdditionalInfo { get; private set; }
        public ICollection<AccommodationPhoto> AccommodationPhotos { get;  }
        public ICollection<AccommodationSpecificity> AccommodationSpecificities { get; }
        public ICollection<AccommodationRule> AccommodationRules { get; }
        public ICollection<AccommodationFacility> AccommodationFacilities { get; }

        public Accommodation(string title, string description, 
            long ownerId, DateTime datePublished, 
            long? categoryId, string address, 
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
            long? categoryId, string address,
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
