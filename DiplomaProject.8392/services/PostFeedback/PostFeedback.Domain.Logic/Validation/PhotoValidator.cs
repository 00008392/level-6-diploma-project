using FluentValidation;
using PostFeedback.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Validation
{
    //validation for post photos
    public class PhotoValidator: AbstractValidator<PhotoDTO>
    {
        public PhotoValidator()
        {
            RuleFor(x => x.Photo)
                .NotEmpty().WithMessage("Photo is required");
        }
    }
}
