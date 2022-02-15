using FluentValidation;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Validation
{
    //validation for feedback creation 
    public class FeedbackValidator: AbstractValidator<FeedbackDTO>
    {
        public FeedbackValidator()
        {
            RuleFor(x => x.CreatorId)
                .NotEmpty().WithMessage("Creator is required");
            RuleFor(x => x.ItemId)
                .NotEmpty().WithMessage("Item is required");
            RuleFor(x=> x.Rating)
                .NotEmpty().WithMessage("Rating is required")
                .Must(x => (x >= 1) && (x <= 5)).WithMessage("Rating value can be between 1 and 5");
            RuleFor(x => x.Message)
                .MaximumLength(200).WithMessage("Maximum message length is 100 symbols");
        }
    }
}
