using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Exceptions
{
    public class AccountNotFoundException: Exception
    {
        public AccountNotFoundException(long id):base($"Account with id = {id} does not exist")
        {

        }
    }
}
