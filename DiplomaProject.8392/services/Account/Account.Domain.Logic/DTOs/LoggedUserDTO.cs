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
        public long Id { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
    }
}
