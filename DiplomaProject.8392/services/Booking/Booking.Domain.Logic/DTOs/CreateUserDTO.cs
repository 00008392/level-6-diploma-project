using Booking.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    public class CreateUserDTO: ICreateEntityDTO
    {
        public string Email { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }

        public CreateUserDTO(string email, string firstName, string lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
