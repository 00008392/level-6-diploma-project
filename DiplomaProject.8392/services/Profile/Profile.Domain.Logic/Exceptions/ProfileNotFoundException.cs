using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.Exceptions
{
    public class ProfileNotFoundException: Exception
    {
        public ProfileNotFoundException(long id): base($"Profile with id = {id} does not exist")
        {

        }
    }
}
