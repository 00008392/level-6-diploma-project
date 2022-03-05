using FrontEndApp.Enums;
using FrontEndApp.Models.Booking.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Booking
{
    public class BookingResponse: BookingBase
    {
        public long Id { get; set; }
        public long GuestId { get; set; }
        public Guest Guest { get; set; }
        public Post Post { get; set; }
        public Status Status { get; set; }
        public bool NoItem { get; set; } = false;
    }
}
