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
    public class AccommodationsByCategorySpecification : AccommodationByItemSpecification
    {
        public AccommodationsByCategorySpecification(long categoryId):base(categoryId)
        {
        }
        public override Expression<Func<Accommodation, bool>> ToExpression()
        {
            return request => request.CategoryId == _itemId;
        }
    }
}
