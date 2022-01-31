using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Specifications.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Specifications
{
    public class PriceSpecification : MinMaxSpecification
    {
        public PriceSpecification(int? minNumber = null, int? maxNumber = null) 
            : base(minNumber, maxNumber)
        {
        }

        public override Expression<Func<Post, bool>> ToExpression()
        {
            return request => (_maxNumber == null || request.Price <= _maxNumber)
             && (_minNumber == null || request.Price >= _minNumber);
        }
    }
}
