using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Exceptions
{
    public class AccommodationBookedException: Exception
    {
        public AccommodationBookedException(long id):
            base($"Accommodation with id={id} is already booked for these dates")
        {

        }
    }
}
