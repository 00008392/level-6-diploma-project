using BaseClasses.Specifications;
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
        protected readonly long _itemId;
        protected PostsByItemSpecification(long itemId)
        {
            _itemId = itemId;
        }
    }
}
