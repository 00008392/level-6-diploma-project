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
        //specification that filters posts by number of guests that accommodation can have 
        private readonly int _guestNumber;

        public NumberOfGuestsSpecification(int guestNumber)
        {
            _guestNumber = guestNumber;
        }

        public override Expression<Func<Post, bool>> ToExpression()
        {
            //get all posts where maximum number of guests is equal to or greater than specified number
            return request => request.MaxGuestsNo >= _guestNumber;
        }
    }
}
