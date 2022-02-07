using Account.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    //dto which is returned by login service when user successfully signs in
    public class LoggedUserDTO
    {
        public long Id { get; private set; }
        public string Email { get;private set; }

        public LoggedUserDTO(long id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
