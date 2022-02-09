using Account.Domain.Logic.IntegrationEvents.Events;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Validation
{
    //validation for booking
    public class BookingValidator : AbstractValidator<BookingAcceptedIntegrationEvent>
    {
        public BookingValidator()
        {
            RuleFor(x => x.BookingId)
                 .NotEmpty().WithMessage("Booking is required");
            RuleFor(x => x.GuestId)
                 .NotEmpty().WithMessage("Guest is required");
            RuleFor(x => x.OwnerId)
                 .NotEmpty().WithMessage("Owner is required");
            RuleFor(x => x.StartDate)
                 .NotEmpty().WithMessage("Start date is required");
            RuleFor(x => x.EndDate)
                 .NotEmpty().WithMessage("End date is required");
        }
       
    }
}
