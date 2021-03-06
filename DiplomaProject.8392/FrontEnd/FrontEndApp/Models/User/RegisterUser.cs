using FrontEndApp.Enums;
using FrontEndApp.Models.Core;
using FrontEndApp.Models.User.Core;
using FrontEndApp.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.User
{
    public class RegisterUser: UserBase
    {
        [Required(ErrorMessage = "Password is required")]
        [PasswordValidation(ErrorMessage = "Password should be 10 characters long, " +
            "and should contain at least 1 digit, 1 symbol, 1 uppercase and 1 lowercase letters")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Confirm password is required")]
        [Compare("Password", ErrorMessage ="Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
