using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class AccommodationRule : ItemAccommodationBase
    {

        public AccommodationRule(long accommodationId, long itemId, string otherItem) 
            : base(accommodationId,itemId, otherItem)
        {
        }

    }
}
