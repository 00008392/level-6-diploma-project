using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Exceptions
{
    //this exception is thrown when user attempts to leave feedback and fails feedback service validation
    public class LeaveFeedbackException: Exception
    {
        public LeaveFeedbackException(long userId, long itemId):base($"User with id={userId} cannot leave feedback on " +
            $"item with id={itemId}")
        {

        }
    }
}
