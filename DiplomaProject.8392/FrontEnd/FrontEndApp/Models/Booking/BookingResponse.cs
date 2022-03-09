using FrontEndApp.Enums;
using FrontEndApp.Models.Booking.Core;
using FrontEndApp.Models.Core;
using FrontEndApp.Models.Post;
using FrontEndApp.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Booking
{
    public class BookingResponse: BookingBase, IResponse
    {
        public long Id { get; set; }
        public long GuestId { get; set; }
        public UserResponse Guest { get; set; }
        public PostResponse Post { get; set; }
        public Status Status { get; set; }
        public bool NoItem { get; set; } = false;
    }
}
