using FluentValidation;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Validation
{
    //validation for post creation and modification
   public class PostValidator: AbstractValidator<PostManipulationDTO>
    {
        public PostValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(50).WithMessage("Maximum title length is 50 symbols");
            RuleFor(p => p.OwnerId)
                .NotEmpty().WithMessage("Owner is required");
            RuleFor(p => p.CityId)
               .NotEmpty().WithMessage("City is required");
            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("Address is required");
            RuleFor(p => p.ContactNumber)
                .NotEmpty().WithMessage("Contact number is required");
            RuleFor(p => p.BedsNo)
                .NotEmpty().WithMessage("Number of beds is required");
            RuleFor(p => p.MaxGuestsNo)
                .NotEmpty().WithMessage("Maximum guest number is required");
            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Price is required");
            RuleFor(p => p.MovingInTime)
                .NotEmpty().WithMessage("Moving in time is required");
            RuleFor(p => p.MovingOutTime)
                .NotEmpty().WithMessage("Moving out time is required");
        }
    }
}
