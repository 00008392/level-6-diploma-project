using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Exceptions
{
    //exception thrown when empty file is submitted
    public class EmptyFileException: Exception
    {
        public EmptyFileException():base("File is empty")
        {

        }
    }
}
