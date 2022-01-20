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
    public class IsWholeApartmentSpecification : Specification<Accommodation>
    {
        private readonly bool _isWholeApartment;
        public IsWholeApartmentSpecification(bool isWholeApartment)
        {
            _isWholeApartment = isWholeApartment;
        }
        public override Expression<Func<Accommodation, bool>> ToExpression()
        {
            return request => request.IsWholeApartment == _isWholeApartment;
        }
    }
}
