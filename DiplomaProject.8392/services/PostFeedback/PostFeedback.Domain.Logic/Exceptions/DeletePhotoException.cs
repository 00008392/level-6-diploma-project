using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Exceptions
{
    public class DeletePhotoException: Exception
    {
        public DeletePhotoException(long id):base($"Photo with id={id} cannot be deleted")
        {

        }
    }
}
