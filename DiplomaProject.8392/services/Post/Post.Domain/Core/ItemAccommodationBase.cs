using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Core
{
    //for bridge tables (rule, facility, specificity)
   public abstract class ItemAccommodationBase: AccommodationBase
    {
        public long ItemId { get; set; }
        public string OtherItem { get; set; }
    }
}
