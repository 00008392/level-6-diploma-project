using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.States
{
    //logged in state stores whether user is authenticated or not
    public class LoggedInState
    {
        public bool isAuthenticated { get; set; } = false;
    }
}
