using Post.Domain.Core;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
   public class AccommodationInfoDTO: BaseAccommodationDTO
    {
        public long Id { get; private set; }
        public Owner Owner { get; private set; }
        public DateTime DatePublished { get; private set; }
        public Category Category { get; private set; }
        public string MovingInTime { get; private set; }
        public string MovingOutTime { get; private set; }
        public ICollection<AccommodationPhoto> AccommodationPhotos { get; private set; }
        public ICollection<AccommodationSpecificity> AccommodationSpecificities { get; private set; }
        public ICollection<AccommodationRule> AccommodationRules { get; private set; }
        public ICollection<AccommodationFacility> AccommodationFacilities { get; private set; }
        public AccommodationInfoDTO(long id, string title, string description,
    long ownerId, long? categoryId,
    string address, string referencePoint,
    string contactNumber, int? roomsNo,
    int? bathroomsNo, int? bedsNo,
    int maxGuestsNo, int? squareMeters,
    decimal price, decimal? latitude,
    decimal? longitude, bool? isWholeApartment,
    string additionalInfo, Owner owner, DateTime datePublished,
    Category category, string movingInTime,
    string movingOutTime, ICollection<AccommodationPhoto> accommodationPhotos,
    ICollection<AccommodationSpecificity> accommodationSpecificities,
    ICollection<AccommodationRule> accommodationRules,
    ICollection<AccommodationFacility> accommodationFacilities) : base(title, description,
        ownerId, categoryId, address, referencePoint,
        contactNumber, roomsNo, bathroomsNo, bedsNo,
        maxGuestsNo, squareMeters, price, latitude,
        longitude, isWholeApartment, additionalInfo)
        {
            Id = id;
            Owner = owner;
            DatePublished = datePublished;
            Category = category;
            MovingInTime = movingInTime;
            MovingOutTime = movingOutTime;
            AccommodationPhotos = accommodationPhotos;
            AccommodationSpecificities = accommodationSpecificities;
            AccommodationRules = accommodationRules;
            AccommodationFacilities = accommodationFacilities;
        }

    }
}
