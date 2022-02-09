using FluentValidation;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Validation
{
    //validation for booking
    //used when BookingAcceptedIntegrationEvent is fired to add booking to post microservice
    public class BookingValidator: AbstractValidator<AddBookingDTO>
    {
        public BookingValidator()
        {
            RuleFor(x => x.BookingId)
                .NotEmpty().WithMessage("Booking id is required");
            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("Post is required");
            RuleFor(x => x.GuestId)
               .NotEmpty().WithMessage("Guest is required");
            RuleFor(x => x.BookingId)
               .NotEmpty().WithMessage("Booking id is required");
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required");
            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required");
        }
    }
}
