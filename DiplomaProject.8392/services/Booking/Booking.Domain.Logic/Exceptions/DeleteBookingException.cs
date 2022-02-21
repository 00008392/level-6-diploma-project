using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Exceptions
{
    //this exception is thrown when user tries to delete booking with status other than pending 
    //or rejected
    public class DeleteBookingException: Exception
    {
        public DeleteBookingException()
            :base("Booking cannot be deleted")
        {

        }
    }
}
