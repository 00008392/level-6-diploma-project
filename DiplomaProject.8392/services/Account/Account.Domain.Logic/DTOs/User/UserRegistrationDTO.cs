using Account.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    //dto for user registration
    public class UserRegistrationDTO: UserBaseDTO, IPasswordBaseDTO
    {
        public string Password { get ; set ; }

        public UserRegistrationDTO(
            string email,
            string firstName,
            string lastName,
            DateTime? dateOfBirth,
            Gender? gender,
            long countryId,
            string password):base(
                firstName,
                lastName,
                email,
                dateOfBirth,
                gender,
                countryId)
        {
            Password = password;
        }
    }
}
