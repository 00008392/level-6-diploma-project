using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic.Base.Exceptions
{
    public class ForeignKeyViolationException : Exception
    {
        public ForeignKeyViolationException(string property) : base($"Invalid {property}")
        {

        }
    }
}
