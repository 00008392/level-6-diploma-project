using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API
{
    //modify to return JWT token along with user
    public partial class LoginResponse
    {
        public string JWTToken { get; set; }
    }
}
