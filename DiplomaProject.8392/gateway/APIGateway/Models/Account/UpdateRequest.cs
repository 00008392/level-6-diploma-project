using APIGateway.Models.Account.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Models.Account
{
    public class UpdateRequest: AccountBase
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public long? CityId { get; set; }
        public string UserInfo { get; set; }
    }
}
