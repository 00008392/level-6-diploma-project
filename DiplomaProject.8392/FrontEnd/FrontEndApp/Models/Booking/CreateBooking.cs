using FrontEndApp.Models.Booking.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Booking
{
    public class CreateBooking: BookingBase
    {
        public long? PostId { get; set; }

    }
}
