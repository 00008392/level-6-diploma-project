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
        public string Email { get; private set; }
        public Role? Role { get;private set; }

        public UserRegistrationDTO(string email, Role? role, string password)
            :base(password)
        {
            Email = email;
            Role = role;
        }
    }
}
