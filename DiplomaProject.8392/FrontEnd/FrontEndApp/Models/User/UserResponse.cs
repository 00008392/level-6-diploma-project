using FrontEndApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.User
{
    public class UserResponse: UpdateUser
    {
        public DateTime RegistrationDate { get; set; }
        public string CountryName { get; set; }
        public bool NoItem { get; set; } = false;
    }
}
