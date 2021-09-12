using Booking.Domain.Entities;
using Booking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    public class BookingRequestInfoDTO
    {
        public UserDTO Guest { get; private set; }
        public BaseAccommodationDTO Accommodation { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Status Status { get; private set; }

        public BookingRequestInfoDTO(UserDTO guest, BaseAccommodationDTO accommodation, 
            DateTime startDate, DateTime endDate, Status status)
        {
            Guest = guest;
            Accommodation = accommodation;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }
    }
}
