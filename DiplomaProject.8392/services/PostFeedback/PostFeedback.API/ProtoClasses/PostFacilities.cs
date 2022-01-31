using PostFeedback.API.ProtoClasses.Core;
using PostFeedback.API.Services.Strategies;
using PostFeedback.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    public static partial class PostFacilities
    {
        public abstract partial class PostFacilitiesBase :
            PostItemsBase<PostFacility, Facility>
        {
            protected PostFacilitiesBase(IPostItemsStrategy<PostFacility, Facility> strategy)
                : base(strategy)
            {
            }
        }
    }
}
