using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.DTOs
{
    public class CreateProfileDTO
    {
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
