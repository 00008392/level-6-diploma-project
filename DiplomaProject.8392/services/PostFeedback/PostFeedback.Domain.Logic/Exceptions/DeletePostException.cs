using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Exceptions
{
    //this exception is thrown when there is an attempt to delete post about accommodation which is booked
    public class DeletePostException: Exception
    {
        public DeletePostException(long id)
            :base($"Accommodation with id = {id} cannot be deleted because it is booked")
        {

        }
    }
}
