using PostFeedback.API.ProtoClasses.Core;
using PostFeedback.API.Services.Strategies;
using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    public static partial class PostRules
    {
        public abstract partial class PostRulesBase
            : PostItemsBase<PostRule, Rule>
        {
            protected PostRulesBase(IPostItemsStrategy<PostRule, Rule> strategy)
                : base(strategy)
            {
            }
        }
    }
}
