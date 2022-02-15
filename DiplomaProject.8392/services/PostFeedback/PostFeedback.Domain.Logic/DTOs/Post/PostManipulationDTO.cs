using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    //dto for post creation and modification
    public class PostManipulationDTO: PostBaseDTO
    {
        public long Id { get; private set; }
        public long OwnerId { get; private set; }
        public DateTime? MovingInTime { get; private set; }
        public DateTime? MovingOutTime { get; private set; }
        //rules and facilities that are associated with the post
        public ICollection<long> Rules { get; private set; }
        public ICollection<long> Facilities { get; private set; }
        //this constructor is called when new post is created
        public PostManipulationDTO(
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
            DateTime? movingInTime,
            DateTime? movingOutTime,
            ICollection<long> rules,
            ICollection<long> facilities) : base(
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
            OwnerId = ownerId;
            MovingInTime = movingInTime;
            MovingOutTime = movingOutTime;
            Rules = rules;
            Facilities = facilities;
        }
        //this constructor is called when post is updated
        public PostManipulationDTO(
            long id,
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
            DateTime? movingInTime,
            DateTime? movingOutTime,
            ICollection<long> rules,
            ICollection<long> facilities) : base(
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
            OwnerId = ownerId;
            MovingInTime = movingInTime;
            MovingOutTime = movingOutTime;
            Rules = rules;
            Facilities = facilities;
        }
    }
}
