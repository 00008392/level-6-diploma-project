using Booking.Domain.Logic.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Validation
{
    public class BookingRequestValidator : AbstractValidator<CreateBookingRequestDTO>
    {
        public BookingRequestValidator()
        {
            RuleFor(r => r.AccommodationId)
                .NotEmpty().WithMessage("Accommodation cannot be empty");
            RuleFor(r => r.GuestId)
               .NotEmpty().WithMessage("Guest cannot be empty");
            RuleFor(r => r.StartDate)
               .NotEmpty().WithMessage("Start date cannot be empty");
            RuleFor(r => r.EndDate)
               .NotEmpty().WithMessage("End date cannot be empty");
        }
    }
}
