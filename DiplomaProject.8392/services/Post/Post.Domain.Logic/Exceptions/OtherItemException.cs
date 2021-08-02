using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Exceptions
{
    public class OtherItemException: Exception
    {
        public OtherItemException():base("Other field cannot be indicated for the given item and accommodation combination" +
            ", since Other field is allowed only for items the value of which is Other")
        {

        }
    }
}
