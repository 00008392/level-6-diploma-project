using Post.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
    public class UserDTO: IFeedbackEntityDTO
    {
        public long Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public UserDTO(long id, string firstName, 
            string lastName, string email, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
