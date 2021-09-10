using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
    public class CreateUserDTO
    {
        public string Email { get; private set; }

        public CreateUserDTO(string email)
        {
            Email = email;
        }
    }
}
