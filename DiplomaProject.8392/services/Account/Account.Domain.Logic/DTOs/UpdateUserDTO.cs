using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    public class UpdateUserDTO
    {
        public long Id { get;private set; }
        public string Email { get;private set; }

        public UpdateUserDTO(long id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
