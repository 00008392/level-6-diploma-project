using FluentValidation;
using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Validation
{
    public class PostRelatedInfoValidator: AbstractValidator<PostRelatedInfoBaseDTO>
    {
        public PostRelatedInfoValidator()
        {
            RuleFor(i => i.AccommodationId)
                .NotEmpty().WithMessage("Accommodation cannot be empty");
            RuleFor(i => i.ItemId)
                .NotEmpty().WithMessage("Item cannot be empty");
        }
    }
}
