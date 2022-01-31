using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    public class AddBookingDTO
    {
        public long BookingId { get;private set; }
        public long PostId { get; private set; }
        public long UserId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public AddBookingDTO(
            long bookingId,
            long postId,
            long userId,
            DateTime startDate,
            DateTime endDate)
        {
            BookingId = bookingId;
            PostId = postId;
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
