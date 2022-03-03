using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Validation
{
    public class PasswordValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
           var password = value.ToString();
            return password != null && password.IndexOfAny("!@#$%^&*()".ToCharArray()) >= 0
                 && password != null && password.Any(c => char.IsUpper(c))
                 && password != null && password.Any(c => char.IsLower(c))
                 && password != null && password.Any(c => char.IsDigit(c));

        }

    }
}
