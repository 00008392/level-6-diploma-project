using Post.API.ProtoClasses.Core;
using Post.API.Services.Strategies;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API
{
    public static partial class AccommodationFacilities
    {
        public abstract partial class AccommodationFacilitiesBase :
            AccommodationItemsBase<AccommodationFacility, Facility>
        {
            protected AccommodationFacilitiesBase(IAccommodationItemsStrategy<AccommodationFacility, Facility> strategy)
                : base(strategy)
            {
            }
        }
    }
}
