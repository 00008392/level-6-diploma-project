using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    //dto for adding accepted booking on accommodation
    public class AddBookingDTO
    {
        public long BookingId { get;private set; }
        public long PostId { get; private set; }
        public long GuestId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public AddBookingDTO(
            long bookingId,
            long postId,
            long guestId,
            DateTime startDate,
            DateTime endDate)
        {
            BookingId = bookingId;
            PostId = postId;
            GuestId = guestId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
