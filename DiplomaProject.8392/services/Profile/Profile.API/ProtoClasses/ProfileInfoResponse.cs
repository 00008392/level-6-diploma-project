using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profile.API
{
    public partial class ProfileInfoResponse
    {
        public ProfileInfoResponse(bool noUser)
        {
            NoUser = noUser;
        }
    }
}
