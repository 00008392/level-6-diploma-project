using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    public class AddBookingDTO
    {
        public long BookingId { get; private set; }
        public long GuestId { get; private set; }
        public long OwnerId { get; private set; }

        public AddBookingDTO(long bookingId, long guestId, long ownerId)
        {
            BookingId = bookingId;
            GuestId = guestId;
            OwnerId = ownerId;
        }
    }
}
