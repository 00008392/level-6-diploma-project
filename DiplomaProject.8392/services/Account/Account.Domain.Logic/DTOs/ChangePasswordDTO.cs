using Account.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    public class ChangePasswordDTO: IPasswordBaseDTO
    {
        public long Id { get; private set; }
        public string Password { get; set; }

        public ChangePasswordDTO(long id, string password)
        {
            Id = id;
            Password = password;
        }
    }
}
