using FrontEndApp.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class UserInfo
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [JsonProperty("DateOfBirth")]
        public DateTime DOB { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Gender Gender { get; set; }
        [JsonProperty("UserInfo")]
        public string Info { get; set; }
        public Country Country { get; set; }
    }
}
