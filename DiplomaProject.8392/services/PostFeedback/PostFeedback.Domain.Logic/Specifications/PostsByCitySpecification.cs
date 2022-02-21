
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
    public class PostsByCitySpecification : PostsByItemSpecification
    {
        //specification that filters posts by city id
        public PostsByCitySpecification(long cityId):base(cityId)
        {
        }
        public override Expression<Func<Post, bool>> ToExpression()
        {
            //get all posts with specified city id
            return request => request.CityId == _itemId;
        }
    }
}
