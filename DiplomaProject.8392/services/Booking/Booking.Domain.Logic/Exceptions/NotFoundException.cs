using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(long id, string property) : base($"{property} with id = {id} does not exist")
        {

        }
        public NotFoundException(string property):base($"{property} does not exist")
        {

        }
    }
}
