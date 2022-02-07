
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Booking.API
{
    public partial class BookingDetailsReply
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
