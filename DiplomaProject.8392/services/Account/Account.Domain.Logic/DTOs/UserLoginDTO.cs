using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    public class UserLoginDTO: PasswordBaseDTO
    {
        public string Email { get; private set; }
        public UserLoginDTO(string password, string email) : base(password)
        {
            Email = email;
        }
    }
}
