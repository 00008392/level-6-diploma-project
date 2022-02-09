using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //base class for rule and facility entities
    //further rule and facility will be called "items" in this code
    public abstract class Item: BaseEntity
    {
        public string Name { get; protected set; }
        //when specifying items for post, user can 
        //either choose from existing options or to define own items
        //but these custom items will not be stored directly in Items table 
        //because this table stores only predefined items
        //instead, custom items will be stored in bridge table that has id of post and id of item
        //while in Items table item with name "Other" and IsOther = true will be stored
        //thereby, when user specifies custom item, its name will be stored in bridge table
        //and this record will refer to item with name "Other" and IsOther = true in Item table
        public bool? IsOther { get; protected set; }
        public ICollection<PostItem> PostItems { get; protected set; }

        protected Item(
            string name,
            bool? isOther)
        {
            Name = name;
            IsOther = isOther;
        }
        protected Item(
            long id,
            string name,
            bool? isOther)
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
