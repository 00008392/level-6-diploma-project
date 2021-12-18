using FluentValidation;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Validation
{
    public class DatesBookedValidator: AbstractValidator<AddBookingDTO>
    {
        public DatesBookedValidator()
        {
            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("Start date cannot be empty");
            RuleFor(x => x.EndDate)
                .NotNull().WithMessage("End date cannot be empty");
        }
    }
}
