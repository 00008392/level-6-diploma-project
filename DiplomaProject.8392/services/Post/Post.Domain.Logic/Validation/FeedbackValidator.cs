using FluentValidation;
using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Validation
{
    public class FeedbackValidator: AbstractValidator<FeedbackDTO>
    {
        public FeedbackValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User id cannot be empty");
            RuleFor(x => x.ItemId)
                .NotEmpty().WithMessage("Item id cannot be empty");
            RuleFor(x=> x.Rating)
                .NotEmpty().WithMessage("Rating cannot be empty")
                .Must(x => (x >= 1) && (x <= 5)).WithMessage("Rating value can be between 1 and 5");
        }
    }
}
