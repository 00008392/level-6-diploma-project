using Booking.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.DTOs
{
    public class UserDTO: CreateUserDTO, IEntityDTO
    {
        public long Id { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }

        public UserDTO(long id, string firstName, string lastName,
           string email, string phoneNumber, string address)
            :base(email, firstName, lastName)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            Address = address;
        }

    }
}
