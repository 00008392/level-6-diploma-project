using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
   public class UpdateUserDTO: CreateUserDTO
    {
        public long Id { get;private set; }
        public string PhoneNumber { get; private set; }

        public UpdateUserDTO(long id, string firstName,
            string lastName, string phoneNumber, string email)
            :base(email, firstName, lastName)
        {
            Id = id;
            PhoneNumber = phoneNumber;
        }
    }
}
