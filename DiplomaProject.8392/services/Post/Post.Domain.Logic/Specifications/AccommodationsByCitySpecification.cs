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
    public class AccommodationsByCitySpecification : AccommodationByItemSpecification
    {
        public AccommodationsByCitySpecification(long cityId):base(cityId)
        {
        }
        public override Expression<Func<Accommodation, bool>> ToExpression()
        {
            return request => request.CityId == _itemId;
        }
    }
}
