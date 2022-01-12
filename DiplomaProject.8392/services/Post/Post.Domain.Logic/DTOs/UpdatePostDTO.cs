﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
    public class UpdatePostDTO: AccommodationManipulationDTO
    {

        public long Id { get; private set; }
        public UpdatePostDTO(long id, string title,
    string description, long ownerId,
    long? categoryId, long? cityId, string address,
    string referencePoint, string contactNumber,
    int? roomsNo, int? bathroomsNo, int? bedsNo,
    int maxGuestsNo, int? squareMeters,
    decimal price, decimal? latitude,
    decimal? longitude, bool? isWholeApartment,
    string additionalInfo, DateTime? movingInTime,
    DateTime? movingOutTime) : base(title, description,
        ownerId, categoryId, cityId, address, referencePoint,
        contactNumber, roomsNo, bathroomsNo, bedsNo,
        maxGuestsNo, squareMeters, price, latitude,
        longitude, isWholeApartment, additionalInfo,
        movingInTime, movingOutTime)
        {
            Id = id;
        }

    }
}
