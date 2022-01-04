using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Exceptions
{
    public class DuplicateItemException: Exception
    {
        public DuplicateItemException(string property, long id)
            :base($"{property} with id={id} already exists in collection")
        {

        } 
    }
}
