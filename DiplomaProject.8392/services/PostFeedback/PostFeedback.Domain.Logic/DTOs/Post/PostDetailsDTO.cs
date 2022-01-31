using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
   public class PostDetailsDTO: PostBaseDTO, IFeedbackEntityDTO
    {
        public long Id { get; private set; }
        public UserDTO Owner { get; private set; }
        public DateTime DatePublished { get; private set; }
        public CategoryCityDTO Category { get; private set; }
        public CategoryCityDTO City { get; private set; }
        public string MovingInTime { get; private set; }
        public string MovingOutTime { get; private set; }
        public ICollection<PhotoDTO> Photos { get; private set; }
        public ICollection<PostItemInfoDTO> Specificities { get; private set; }
        public ICollection<PostItemInfoDTO> Rules { get; private set; }
        public ICollection<PostItemInfoDTO> Facilities { get; private set; }
        public PostDetailsDTO(long id, string title, string description,
    string address, string referencePoint,
    string contactNumber, int? roomsNo,
    int? bathroomsNo, int? bedsNo,
    int maxGuestsNo, int? squareMeters,
    decimal price, 
    bool? isWholeApartment,
    string additionalInfo, UserDTO owner, DateTime datePublished,
    CategoryCityDTO category, CategoryCityDTO city, string movingInTime,
    string movingOutTime, ICollection<PhotoDTO> photos,
    ICollection<PostItemInfoDTO> specificities,
    ICollection<PostItemInfoDTO> rules,
    ICollection<PostItemInfoDTO> facilities) : base(title, description,
        address, referencePoint,
        contactNumber, roomsNo, bathroomsNo, bedsNo,
        maxGuestsNo, squareMeters, price, 
        isWholeApartment, additionalInfo)
        {
            Id = id;
            Owner = owner;
            DatePublished = datePublished;
            Category = category;
            City = city;
            MovingInTime = movingInTime;
            MovingOutTime = movingOutTime;
            Photos = photos;
            Specificities = specificities;
            Rules = rules;
            Facilities = facilities;
        }

    }
}
