using Account.Domain.Entities;
using Account.Domain.Logic.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Validation
{
    //validation for registration
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationDTO>
    {
        public UserRegistrationValidator(
            AbstractValidator<IPasswordBaseDTO> pwdValidator, 
            AbstractValidator<UserBaseDTO> userValidator
            )
        {
            //includes validation of password
            Include(pwdValidator);
            //includes validaiton of basic user properties
            Include(userValidator);
        }

    }
}
