using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Exceptions
{
    public class DuplicateItemException: Exception
    {
        public DuplicateItemException(object value)
            : base($"Item with id = {value} already exists for given accommodation")
        {

        }
    }
}
