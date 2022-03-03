using FrontEndApp.Enums;
using FrontEndApp.Models.User.Core;
using FrontEndApp.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.User
{
    public class UpdateUser: UserBase
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string UserInfo { get; set; }
    }
}
