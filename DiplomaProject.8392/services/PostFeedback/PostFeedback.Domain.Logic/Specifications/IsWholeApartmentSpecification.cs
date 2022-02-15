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
    public class IsWholeApartmentSpecification : Specification<Post>
    {
        //specification that filter posts by availability type
        //(is whole accommodation available or only its part)
        private readonly bool _isWholeApartment;
        public IsWholeApartmentSpecification(bool isWholeApartment)
        {
            _isWholeApartment = isWholeApartment;
        }
        public override Expression<Func<Post, bool>> ToExpression()
        {
            return request => request.IsWholeApartment == _isWholeApartment;
        }
    }
}
