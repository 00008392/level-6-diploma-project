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
        //specification that filters posts by price range
        public PriceSpecification(int? minNumber = null, int? maxNumber = null) 
            : base(minNumber, maxNumber)
        {
        }

        public override Expression<Func<Post, bool>> ToExpression()
        {
            //get all posts where price is less than or
            //equal to specified maximum price and greater than or equal to specified minimum price
            return request => (_maxNumber == null || request.Price <= _maxNumber)
             && (_minNumber == null || request.Price >= _minNumber);
        }
    }
}
