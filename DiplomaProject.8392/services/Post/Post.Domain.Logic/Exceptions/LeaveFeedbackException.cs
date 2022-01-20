using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Exceptions
{
    public class LeaveFeedbackException: Exception
    {
        public LeaveFeedbackException(long userId, long itemId):base($"User with id={userId} cannot leave feedback on " +
            $"item with id={itemId}")
        {

        }
    }
}
