using BaseClasses.Specifications;
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
    public class PostsByCategorySpecification : PostsByItemSpecification
    {
        //specification that filters posts by category id
        public PostsByCategorySpecification(long categoryId):base(categoryId)
        {
        }
        public override Expression<Func<Post, bool>> ToExpression()
        {
            //get all posts with specified category id
            return request => request.CategoryId == _itemId;
        }
    }
}
