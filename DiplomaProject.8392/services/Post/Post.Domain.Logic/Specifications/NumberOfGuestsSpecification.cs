using BaseClasses.Specifications;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Specifications
{
    public class NumberOfGuestsSpecification : Specification<Accommodation>
    {
        private readonly int _guestNumber;

        public NumberOfGuestsSpecification(int guestNumber)
        {
            _guestNumber = guestNumber;
        }

        public override Expression<Func<Accommodation, bool>> ToExpression()
        {
            return request => request.MaxGuestsNo >= _guestNumber;
        }
    }
}
