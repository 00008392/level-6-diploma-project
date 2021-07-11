using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.DTOs
{
    public class ChangePasswordDTO
    {
        public long Id { get; set; }
        public string Password { get; set; }
    }
}
