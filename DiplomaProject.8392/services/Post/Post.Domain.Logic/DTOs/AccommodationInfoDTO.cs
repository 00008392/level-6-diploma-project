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
        public UserDTO Owner { get; private set; }
        public DateTime DatePublished { get; private set; }
        public CategoryDTO Category { get; private set; }
        public string MovingInTime { get; private set; }
        public string MovingOutTime { get; private set; }
        public ICollection<AccommodationPhotoDTO> AccommodationPhotos { get; private set; }
        public ICollection<AccommodationItemInfoDTO> AccommodationSpecificities { get; private set; }
        public ICollection<AccommodationItemInfoDTO> AccommodationRules { get; private set; }
        public ICollection<AccommodationItemInfoDTO> AccommodationFacilities { get; private set; }
        public AccommodationInfoDTO(long id, string title, string description,
    long ownerId, long? categoryId,
    string address, string referencePoint,
    string contactNumber, int? roomsNo,
    int? bathroomsNo, int? bedsNo,
    int maxGuestsNo, int? squareMeters,
    decimal price, decimal? latitude,
    decimal? longitude, bool? isWholeApartment,
    string additionalInfo, UserDTO owner, DateTime datePublished,
    CategoryDTO category, string movingInTime,
    string movingOutTime, ICollection<AccommodationPhotoDTO> accommodationPhotos,
    ICollection<AccommodationItemInfoDTO> accommodationSpecificities,
    ICollection<AccommodationItemInfoDTO> accommodationRules,
    ICollection<AccommodationItemInfoDTO> accommodationFacilities) : base(title, description,
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
