using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API
{
    public partial class LoginReply
    {
        public LoginReply(long id, string email, int? role)
        {
            Id = id;
            Email = email;
            Role = role;
        }
        public LoginReply(bool noUser)
        {
            NoUser = noUser;
        } 
    }
}
