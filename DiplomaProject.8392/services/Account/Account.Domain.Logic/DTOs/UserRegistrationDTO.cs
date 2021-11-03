using Account.Domain.Enums;
using Account.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    public class UserRegistrationDTO: UserBaseDTO, IPasswordBaseDTO
    {
        public Role? Role { get; private set; }
        public string Password { get ; set ; }

        public UserRegistrationDTO(
            string email,
            Role? role,
            string firstName,
            string lastName,
            DateTime? dateOfBirth,
            Gender? gender,
            string password):base(firstName, lastName, email, dateOfBirth, gender)
        {
            Role = role;
            Password = password;
        }
    }
}
