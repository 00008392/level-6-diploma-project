using Account.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    public class UserRegistrationDTO: PasswordBaseDTO
    {
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
