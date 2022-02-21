using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Exceptions
{
    //exception thrown when user tries to book accommodation indicated in his own post
   public class AccommodationBookedByOwnerException: Exception
    {
        public AccommodationBookedByOwnerException() :
           base("Accommodation cannot be booked by its owner")
        {

        }
    }
}
