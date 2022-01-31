using FluentValidation;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Validation
{
    public class BookingValidator: AbstractValidator<AddBookingDTO>
    {
        public BookingValidator()
        {
            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("Start date cannot be empty");
            RuleFor(x => x.EndDate)
                .NotNull().WithMessage("End date cannot be empty");
        }
    }
}
