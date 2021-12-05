using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    public class CoTravelerDTO
    {
        public long BookingId { get; private set; }
        public long CoTravelerId { get; private set; }

        public CoTravelerDTO(long bookingId, long coTravelerId)
        {
            BookingId = bookingId;
            CoTravelerId = coTravelerId;
        }
    }
}
