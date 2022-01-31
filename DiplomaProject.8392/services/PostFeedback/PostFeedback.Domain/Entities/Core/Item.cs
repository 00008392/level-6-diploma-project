using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //for rule/facility/speecificity
    public abstract class Item: BaseEntity
    {
        public string Name { get; protected set; }
        public bool? IsOther { get; protected set; }
        public ICollection<PostItem> PostItems { get; }

        protected Item(string name, bool? isOther)
        {
            Name = name;
            IsOther = isOther;
        }
        protected Item(long id, string name, bool? isOther)
            :base(id)
        {
            Name = name;
            IsOther = isOther;
        }
        protected Item()
        {

        }
    }


}
