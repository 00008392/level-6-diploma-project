using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Exceptions
{
    //exception thrown when user tries to delete account while having active bookings
   public class DeleteUserException: Exception
    {
        public DeleteUserException()
            :base("Account cannot be deleted, because it has active bookings")
        {

        }
    }
}
