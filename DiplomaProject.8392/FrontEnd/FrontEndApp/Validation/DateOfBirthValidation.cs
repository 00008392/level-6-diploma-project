using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Validation
{
    public class DateOfBirthValidation : ValidationAttribute
    {
        private int _ageLimit;
        public DateOfBirthValidation(int limit)
        {
            _ageLimit = limit;
        }
        public override bool IsValid(object value)
        {

            DateTime dateTime = Convert.ToDateTime(value);
            if (dateTime.AddYears(_ageLimit) > DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }
}
