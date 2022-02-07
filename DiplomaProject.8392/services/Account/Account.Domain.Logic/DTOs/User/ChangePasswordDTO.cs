
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    //dto for password change
    public class ChangePasswordDTO: IPasswordBaseDTO
    {
        public long UserId { get; private set; }
        public string Password { get; set; }

        public ChangePasswordDTO(
            long userId,
            string password)
        {
            UserId = userId;
            Password = password;
        }
    }
}
