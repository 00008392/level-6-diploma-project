using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //base class for bridge between post and item (rule/facility)
    //needed because of many to many relationships
   public abstract class PostItem: BaseEntity
    {
        public long PostId { get; protected set; }
        public Post Post { get; protected set; }
        public long ItemId { get; protected set; }
        //need this property in case if user specifies custom item value
        public string OtherValue { get; protected set; }
        public Item Item { get; protected set; }

        protected PostItem(
            long postId,
            long itemId,
            string otherValue)
        {
            PostId = postId;
            ItemId = itemId;
            OtherValue = otherValue;
        }
        protected PostItem(
            long id,
            long postId,
            long itemId,
            string otherValue)
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
