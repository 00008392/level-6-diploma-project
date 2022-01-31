using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Exceptions
{
    public class EmptyItemsListException: Exception
    {
        public EmptyItemsListException():base("List of items is empty")
        {

        }
    }
}
