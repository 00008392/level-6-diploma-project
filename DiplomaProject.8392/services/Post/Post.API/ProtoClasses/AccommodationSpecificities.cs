using Post.API.ProtoClasses.Core;
using Post.API.Services.Strategies;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API
{
    public static partial class AccommodationSpecificities
    {
        public abstract partial class AccommodationSpecificitiesBase
            : AccommodationItemsBase<AccommodationSpecificity, Specificity>
        {
            protected AccommodationSpecificitiesBase(IAccommodationItemsStrategy<AccommodationSpecificity, Specificity> strategy)
                : base(strategy)
            {
            }
        }
    }
}
