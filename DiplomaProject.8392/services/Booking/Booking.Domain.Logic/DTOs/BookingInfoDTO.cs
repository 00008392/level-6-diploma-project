using Booking.Domain.Entities;
using Booking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    public class BookingInfoDTO
    {
        public long Id { get;private set; }
        public UserDTO Guest { get; private set; }
        public AccommodationDTO Accommodation { get; private set; }
        public ICollection<UserDTO> CoTravelers { get; private set; }
        public int GuestNo { get;private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Status Status { get; private set; }

        public BookingInfoDTO(long id,
            UserDTO guest, AccommodationDTO accommodation,
            ICollection<UserDTO> coTravelers,
            int guestNo,
            DateTime startDate, DateTime endDate, Status status)
        {
            Id = id;
            Guest = guest;
            Accommodation = accommodation;
            CoTravelers = coTravelers;
            GuestNo = guestNo;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }

    }
}
