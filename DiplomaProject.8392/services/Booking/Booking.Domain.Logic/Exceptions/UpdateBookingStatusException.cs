using Booking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Exceptions
{
    //this exception is thrown when booking status cannot be changed to given status
   public class UpdateBookingStatusException: Exception
    {
        public UpdateBookingStatusException(Status status):
            base($"Status of booking cannot be changed to {status}")
        {

        }
    }
}
