using Booking.Domain.Logic.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Validation
{
    public class UpdateUserValidator:AbstractValidator<UserDTO>
    {
        public UpdateUserValidator(AbstractValidator<CreateUserDTO> validator)
        {
            Include(validator);
        }

       
    }
}
