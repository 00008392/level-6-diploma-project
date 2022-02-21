using Booking.Domain.Logic.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Validation
{
    public class BookingValidator : AbstractValidator<CreateBookingDTO>
    {
        //validation for booking
        public BookingValidator()
        {
            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("Post is required");
            RuleFor(x => x.GuestId)
               .NotEmpty().WithMessage("Guest is required");
            RuleFor(x => x.GuestNo)
               .NotEmpty().WithMessage("Number of guests is required");
            RuleFor(x => x.StartDate)
               .NotEmpty().WithMessage("Start date is required");
            RuleFor(x => x.EndDate)
               .NotEmpty().WithMessage("End date is required");
            When(x => x.StartDate != null && x.EndDate != null, () =>
                    {
                       RuleFor(x => x)
                      .Must(IsEndDateGreaterThanStartDate)
                              .WithMessage("Start date should be earlier than end date")
                      .Must(CheckStartEndDatesDifference)
                              .WithMessage("Minimum length of stay should be 3 days and " +
                              "maximum 30 days")
                      .Must(CheckStartDate)
                              .WithMessage("Booking should be made minimum 5 days and " +
                              "maximum 90 days before indicated start date").WithName("Date range");
                    });
          
        }
        //when user books accomodation, start date and end date should follow requirements:
        //minimum period of time to stay in accommodation is 3 days and maximum is 31 days
        //guest should book accommodation minimum 5 days and maximum 90 days before indicated start date
        private bool IsEndDateGreaterThanStartDate(CreateBookingDTO booking)
        {
            return booking.StartDate < booking.EndDate;
        }
        private bool CheckStartEndDatesDifference(CreateBookingDTO booking)
        {
            var start = (DateTime)booking.StartDate;
            var end = (DateTime)booking.EndDate;
            return (end - start).Days >= 3 && (end - start).Days <= 30;
        }
        private bool CheckStartDate(CreateBookingDTO booking)
        {
            var start = (DateTime)booking.StartDate;
            return (start - DateTime.Now).Days >= 5 && (start - DateTime.Now).Days <= 90;
        }
    }
}
