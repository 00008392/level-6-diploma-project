using Booking.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    public class CreateUserDTO: CreateEntityDTO
    {
        public string Email { get; protected set; }

        public CreateUserDTO(string email)
        {
            Email = email;
        }
    }
}
