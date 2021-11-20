using Post.API.ProtoClasses.Core;
using Post.API.Services.Strategies;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API
{
    public static partial class AccommodationRules
    {
        public abstract partial class AccommodationRulesBase
            : AccommodationItemsBase<AccommodationRule, Rule>
        {
            protected AccommodationRulesBase(IAccommodationItemsStrategy<AccommodationRule, Rule> strategy)
                : base(strategy)
            {
            }
        }
    }
}
