using BaseClasses.Specifications;
using Post.Domain.Entities;
using Post.Domain.Logic.Specifications.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Specifications
{
    public class RoomsNumberSpecification : MinMaxSpecification
    {

        public RoomsNumberSpecification(int? minNumber=null, int? maxNumber=null)
            :base(minNumber, maxNumber)
        {
        }

        public override Expression<Func<Accommodation, bool>> ToExpression()
        {
            return request => (_maxNumber == null || request.RoomsNo <= _maxNumber)
            && (_minNumber == null || request.RoomsNo >= _minNumber);
        }
    }
}
