using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Exceptions
{
    public class InvalidGuestNumberException: Exception
    {
        public InvalidGuestNumberException()
            :base("Maximum number of guests is exceeded")
        {

        }
    }
}
