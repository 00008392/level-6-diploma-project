using BaseClasses.Specifications;
using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Specifications
{
    public class NumberOfGuestsSpecification : Specification<Post>
    {
        private readonly int _guestNumber;

        public NumberOfGuestsSpecification(int guestNumber)
        {
            _guestNumber = guestNumber;
        }

        public override Expression<Func<Post, bool>> ToExpression()
        {
            return request => request.MaxGuestsNo >= _guestNumber;
        }
    }
}
