using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Exceptions
{
    public class DuplicateListValueException: Exception
    {
        public DuplicateListValueException():base($"Duplicate values in given collection")
        {

        }
    }
}
