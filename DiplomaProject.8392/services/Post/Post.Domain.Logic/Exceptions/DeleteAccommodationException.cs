using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Exceptions
{
    public class DeleteAccommodationException: Exception
    {
        public DeleteAccommodationException(long id)
            :base($"Accommodation with id = {id} cannot be deleted because it is booked")
        {

        }
    }
}
