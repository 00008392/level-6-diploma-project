﻿using Booking.Domain.Logic.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Logic.Validation
{
   public class BaseAccommodationValidator: AbstractValidator<BaseAccommodationDTO>
    {
        public BaseAccommodationValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title cannot be empty");
            RuleFor(p => p.OwnerId)
                .NotEmpty().WithMessage("No owner");
            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("Address cannot be empty");
            RuleFor(p => p.ContactNumber)
                .NotEmpty().WithMessage("Contact number cannot be empty");
            RuleFor(p => p.MaxGuestsNo)
                .NotEmpty().WithMessage("Guest number cannot be empty");
            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Price cannot be empty");
            RuleFor(p => p.MovingInTime)
                .NotEmpty().WithMessage("Moving in time cannot be empty");
            RuleFor(p => p.MovingOutTime)
                .NotEmpty().WithMessage("Moving out time cannot be empty");
        }
    }
}