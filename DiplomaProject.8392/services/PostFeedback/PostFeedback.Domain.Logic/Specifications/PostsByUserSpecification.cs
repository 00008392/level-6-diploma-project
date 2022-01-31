﻿using BaseClasses.Specifications;
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
    public class PostsByUserSpecification: PostsByItemSpecification
    {
        public PostsByUserSpecification(long userId):base(userId)
        {
        }
        public override Expression<Func<Post, bool>> ToExpression()
        {
            return request => request.OwnerId == _itemId;
        }
    }
}
