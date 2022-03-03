using FrontEndApp.Enums;
using FrontEndApp.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.User.Core
{
    public class UserBase
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        [DateOfBirthValidation(18, ErrorMessage = "User should be at least 18 years old")]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public Gender? Gender { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public long? CountryId { get; set; }
    }
}
