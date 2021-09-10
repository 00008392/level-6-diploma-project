using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class AccommodationFacility : ItemAccommodationBase
    {

        public AccommodationFacility(long itemId, string otherItem)
            : base(itemId, otherItem)
        {
        }


    }
}
