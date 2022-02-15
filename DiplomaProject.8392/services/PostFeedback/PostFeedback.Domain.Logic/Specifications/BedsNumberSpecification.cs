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
   public class BedsNumberSpecification : MinMaxSpecification
    {
        //specification that filters posts by number of beds range
        public BedsNumberSpecification(int? minNumber = null, int? maxNumber = null)
         : base(minNumber, maxNumber)
        {
        }

        public override Expression<Func<Post, bool>> ToExpression()
        {
            //get all posts where number of beds is greater than or equal to specified minimum
            //and smaller than or equal to specified maximum
            return request => (_maxNumber == null || request.BedsNo <= _maxNumber)
            && (_minNumber == null || request.BedsNo >= _minNumber);
        }
    }
}
