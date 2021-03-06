using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic.Base.Exceptions
{
    public class UniqueConstraintViolationException : Exception
    {
        public UniqueConstraintViolationException(string property, object value) :
            base($"{property} with value {value} already exists")
        {

        }
    }
}
