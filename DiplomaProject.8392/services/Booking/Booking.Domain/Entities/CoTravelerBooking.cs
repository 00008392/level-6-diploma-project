using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class CoTravelerBooking: BaseEntity
    {
        public long BookingId { get; set; }
        public BookingRequest Booking { get; set; }
        public long? CoTravelerId { get; set; }
        public User CoTraveler { get; set; }
    }
}
