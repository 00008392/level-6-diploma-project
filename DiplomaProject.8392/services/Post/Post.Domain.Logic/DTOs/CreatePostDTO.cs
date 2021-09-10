﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
    public class CreatePostDTO : AccommodaitonManipulationDTO
    {
        public CreatePostDTO(string title, string description,
            long ownerId, long? categoryId,
            string address, string referencePoint,
            string contactNumber, int? roomsNo, 
            int? bathroomsNo, int? bedsNo,
            int maxGuestsNo, int? squareMeters, 
            decimal price, decimal? latitude, 
            decimal? longitude, bool? isWholeApartment,
            string additionalInfo, DateTime? movingInTime,
            DateTime? movingOutTime) : base(title, description,
                ownerId, categoryId, address, referencePoint, 
                contactNumber, roomsNo, bathroomsNo, bedsNo, 
                maxGuestsNo, squareMeters, price, latitude,
                longitude, isWholeApartment, additionalInfo, 
                movingInTime, movingOutTime)
        {
        }
    }
}
