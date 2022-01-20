﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
    public class AddBookingDTO
    {
        public long BookingId { get;private set; }
        public long AccommodationId { get; private set; }
        public long UserId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public AddBookingDTO(
            long bookingId,
            long accommodationId,
            long userId,
            DateTime startDate,
            DateTime endDate)
        {
            BookingId = bookingId;
            AccommodationId = accommodationId;
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
