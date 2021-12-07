using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Controllers.Booking.QueryParameters
{
    public class GetBookingsQueryParameter
    {
        public long? User { get; set; }
        public long? Accommodation { get; set; }
    }
}
