using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.DTOs
{
    public class CreateProfileDTO
    {
        public string Email { get;private set; }
        public DateTime RegistrationDate { get;private set; }

        public CreateProfileDTO(string email, DateTime registrationDate)
        {
            Email = email;
            RegistrationDate = registrationDate;
        }
    }
}
