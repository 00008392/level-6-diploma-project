
using Domain.Logic.Base.Specifications;
using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.Specifications.Core
{
    public abstract class PostsByItemSpecification : Specification<Post>
    {
        //base class for specifications that filter posts by id of related entity
        protected readonly long _itemId;
        protected PostsByItemSpecification(long itemId)
        {
            _itemId = itemId;
        }
    }
}
