using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    //dto returned when post information is retrieved
   public class PostDetailsDTO: PostBaseDTO, IFeedbackEntityDTO
    {
        public long Id { get; private set; }
        public UserDTO Owner { get; private set; }
        public DateTime DatePublished { get; private set; }
        public string Category { get; private set; }
        public string City { get; private set; }
        public string MovingInTime { get; private set; }
        public string MovingOutTime { get; private set; }
        public ICollection<ItemDTO> Rules { get; private set; }
        public ICollection<ItemDTO> Facilities { get; private set; }
        public ICollection<DatesBookedDTO> DatesBooked { get; private set; }
        public PostDetailsDTO(
            long id,
            string title,
            string description,
            string address,
            string contactNumber,
            long? categoryId,
            long cityId,
            int roomsNo,
            int? bathroomsNo,
            int bedsNo,
            int maxGuestsNo,
            int? squareMeters,
            decimal price,
            bool isWholeApartment,
            UserDTO owner,
            DateTime datePublished,
            string category,
            string city,
            string movingInTime,
            string movingOutTime,
            ICollection<ItemDTO> rules,
            ICollection<ItemDTO> facilities, 
            ICollection<DatesBookedDTO> datesBooked) : base(
                title,
                description,
                address,
                contactNumber,
                categoryId,
                cityId,
                roomsNo,
                bathroomsNo,
                bedsNo,
                maxGuestsNo,
                squareMeters,
                price,
                isWholeApartment)
        {
            Id = id;
            Owner = owner;
            DatePublished = datePublished;
            Category = category;
            City = city;
            MovingInTime = movingInTime;
            MovingOutTime = movingOutTime;
            Rules = rules;
            Facilities = facilities;
            DatesBooked = datesBooked;
        }

    }
}
