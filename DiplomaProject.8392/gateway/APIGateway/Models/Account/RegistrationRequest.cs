using APIGateway.Models.Account.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Models.Account
{
    public class RegistrationRequest: AccountBase
    {
        public string Password { get; set; }
        public int? Role { get; set; }
    }
}
