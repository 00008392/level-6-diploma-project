using Account.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs
{
    //need this base for fluent validation and for registration/update/user info DTOs
    public abstract class UserBaseDTO
    {
        public string Email { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public DateTime? DateOfBirth { get; protected set; }
        public Gender? Gender { get; protected set; }
        public long CountryId { get; protected set; }

        protected UserBaseDTO(
            string firstName,
            string lastName,
            string email,
            DateTime? dateOfBirth,
            Gender? gender,
            long countryId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            CountryId = countryId;
        }
    }
}
