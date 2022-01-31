using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
   public class PostManipulationDTO: PostBaseDTO
    {
        public long Id { get; private set; }
        public long OwnerId { get; protected set; }
        public long? CategoryId { get; protected set; }
        public long? CityId { get; protected set; }
        public DateTime? MovingInTime { get; private set; }
        public DateTime? MovingOutTime { get; private set; }
        public PostManipulationDTO(string title, string description,
    long ownerId, long? categoryId, long? cityId,
    string address, string referencePoint,
    string contactNumber, int? roomsNo,
    int? bathroomsNo, int? bedsNo,
    int maxGuestsNo, int? squareMeters,
    decimal price, 
     bool? isWholeApartment,
    string additionalInfo, DateTime? movingInTime,
    DateTime? movingOutTime) : base(title, description,
        address,
        referencePoint, contactNumber, roomsNo,
        bathroomsNo, bedsNo, maxGuestsNo,
        squareMeters, price, 
         isWholeApartment, additionalInfo)
        {
            OwnerId = ownerId;
            CategoryId = categoryId;
            CityId = cityId;
            MovingInTime = movingInTime;
            MovingOutTime = movingOutTime;
        }
        public PostManipulationDTO(long id, string title, string description,
   long ownerId, long? categoryId, long? cityId,
   string address, string referencePoint,
   string contactNumber, int? roomsNo,
   int? bathroomsNo, int? bedsNo,
   int maxGuestsNo, int? squareMeters,
   decimal price,
   bool? isWholeApartment,
   string additionalInfo, DateTime? movingInTime,
   DateTime? movingOutTime) : base(title, description,
        address,
       referencePoint, contactNumber, roomsNo,
       bathroomsNo, bedsNo, maxGuestsNo,
       squareMeters, price, 
        isWholeApartment, additionalInfo)
        {
            Id = id;
            OwnerId = ownerId;
            CategoryId = categoryId;
            CityId = cityId;
            MovingInTime = movingInTime;
            MovingOutTime = movingOutTime;
        }
    }
}
