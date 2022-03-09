using FrontEndApp.Enums;
using FrontEndApp.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.User
{
    public class UserResponse: UpdateUser, IFeedbackItem, IResponse
    {
        public DateTime RegistrationDate { get; set; }
        public string CountryName { get; set; }
        public double? AverageRating { get; set; }
        public bool NoItem { get; set; } = false;
    }
}
