using BaseClasses.Entities;
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
        public string Name { get; protected set; }
        public bool? IsOther { get; protected set; }
        public ICollection<ItemAccommodationBase> AccommodationItems { get; }

        protected ItemBase(string name, bool? isOther)
        {
            Name = name;
            IsOther = isOther;
        }
        protected ItemBase()
        {

        }
    }


}
