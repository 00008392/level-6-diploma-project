using Account.Domain.Core;
using Account.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
   public class User: BaseEntity
    {
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Role Role { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
