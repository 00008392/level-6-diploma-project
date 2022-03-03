using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Exceptions
{
    public class NumberOfPhotosExceededException: Exception
    {
        public NumberOfPhotosExceededException():
            base("Number of photos for post exceeds limit of 15 photos")
        {

        }
    }
}
