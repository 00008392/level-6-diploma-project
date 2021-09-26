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
        public AccommodationDTO Accommodation { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Status Status { get; private set; }

        public BookingRequestInfoDTO(
            DateTime startDate, DateTime endDate, Status status)
        {
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }
        public void LoadUser(UserDTO user)
        {
            Guest = user;
        }
        public void LoadAccommodation(AccommodationDTO accommodation)
        {
            Accommodation = accommodation;
        }
    }
}
