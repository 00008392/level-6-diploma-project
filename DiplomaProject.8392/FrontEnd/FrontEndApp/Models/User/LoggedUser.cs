﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class LoggedUser
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string JWTToken { get; set; }
       
    }
}
