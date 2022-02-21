using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    //dto for booking creation
    public class CreateBookingDTO
    {
        public long GuestId { get; private set; }
        public long PostId { get; private set; }
        public int GuestNo { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public CreateBookingDTO(
            long guestId,
            long postId,
            int guestNo,
            DateTime? startDate,
            DateTime? endDate)
        {
            GuestId = guestId;
            PostId = postId;
            GuestNo = guestNo;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
