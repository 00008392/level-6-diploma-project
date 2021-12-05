using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    public class CreateBookingRequestDTO
    {
        public long GuestId { get; private set; }
        public long AccommodationId { get; private set; }
        public int GuestNo { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public CreateBookingRequestDTO(long guestId, long accommodationId, 
            int guestNo,
            DateTime? startDate, DateTime? endDate)
        {
            GuestId = guestId;
            AccommodationId = accommodationId;
            GuestNo = guestNo;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
