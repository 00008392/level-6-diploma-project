using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Exceptions
{
    public class DeleteAccountException: Exception
    {
        public DeleteAccountException(long id):
            base($"Account with id = {id} cannot be deleted because it has bookings")
        {

        }
    }
}
