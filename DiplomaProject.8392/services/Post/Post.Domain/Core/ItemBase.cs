using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Core
{
    //for rule/facility/speecificity
    public abstract class ItemBase: BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ItemAccommodationBase> AccommodationItems { get; set; }
    }


}
