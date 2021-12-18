using FluentValidation;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Validation
{
   public class UpdateUserValidator: AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator(AbstractValidator<CreateUserDTO> validator)
        {
            Include(validator);
            //RuleFor(u => u.DateOfBirth)
            //    .NotNull().WithMessage("Date of birth cannot be empty")
            //    .Must(IsAdult).WithMessage("You should be older than 18");
        }
        //private bool IsAdult(DateTime? dateOfBirth)
        //{
        //    if (dateOfBirth == null)
        //    {
        //        return false;
        //    }
        //    if (((DateTime)dateOfBirth).AddYears(18) > DateTime.Now)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
    }
}
