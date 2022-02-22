using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Exceptions
{
    //exception thrown when file of invalid type is submitted
    public class FileContentTypeException: Exception
    {
        public FileContentTypeException():base("Invalid file format")
        {

        }
    }
}
