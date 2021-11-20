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
           
        }
    }
}
