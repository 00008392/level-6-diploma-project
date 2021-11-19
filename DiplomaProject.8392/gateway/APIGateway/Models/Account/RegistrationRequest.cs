
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API
{
    public partial class RegistrationRequest: IAccountRequest
    {
        public DateTime? DateOfBirth { get; set; }
    }
}
