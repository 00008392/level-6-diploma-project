using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //for bridge tables (rule, facility, specificity)
   public abstract class PostItem: BaseEntity
    {
        public long PostId { get; protected set; }
        public Post Post { get; }
        public long ItemId { get; protected set; }
        public string OtherValue { get; protected set; }
        public Item Item { get;}

        protected PostItem(long postId, long itemId, string otherValue)
        {
            PostId = postId;
            ItemId = itemId;
            OtherValue = otherValue;
        }
        protected PostItem(long id, long postId, long itemId, string otherValue)
            :base(id)
        {
            PostId = postId;
            ItemId = itemId;
            OtherValue = otherValue;
        }
        protected PostItem()
        {
        }
    }
}
