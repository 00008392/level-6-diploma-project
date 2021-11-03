using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Core
{
    //for bridge tables (rule, facility, specificity)
   public abstract class ItemAccommodationBase: AccommodationEntityBase
    {
        public long ItemId { get; protected set; }
        public string OtherItem { get; protected set; }
        public ItemBase Item { get;}

        protected ItemAccommodationBase(long accommodationId, long itemId, string otherItem)
            :base(accommodationId)
        {
            ItemId = itemId;
            OtherItem = otherItem;
        }
        protected ItemAccommodationBase()
        {
        }
    }
}
