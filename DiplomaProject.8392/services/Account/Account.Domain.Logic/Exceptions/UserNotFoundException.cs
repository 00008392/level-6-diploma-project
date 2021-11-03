using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(long id) : base($"User with id = {id} does not exist")
        {

        }
    }
}
