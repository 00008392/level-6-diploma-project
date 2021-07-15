using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    public class ChangePasswordDTO: PasswordBaseDTO
    {
        public long Id { get; set; }
    }
}
