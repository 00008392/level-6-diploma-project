using Account.API;
using APIGateway.Models.Account.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Models.Account
{
    public class UserInfoResponse: AccountBase
    {
        public long Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserInfo { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public string MimeType { get; set; }
    }
}
