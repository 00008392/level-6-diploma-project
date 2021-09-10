using Account.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    public class LoggedUserDTO
    {
        public long Id { get; private set; }
        public string Email { get;private set; }
        public int Role { get;private set; }

        public LoggedUserDTO(long id, string email, int role)
        {
            Id = id;
            Email = email;
            Role = role;
        }
    }
}
