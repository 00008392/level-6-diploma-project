using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    public class UpdateUserDTO
    {
        public long Id { get; set; }
        public string Email { get; set; }
    }
}
