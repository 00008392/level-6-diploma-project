
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    //dto for holding user login credentials
    public class UserLoginDTO
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public UserLoginDTO(
            string password,
            string email) 
        {
            Email = email;
            Password = password;
        }
    }
}
