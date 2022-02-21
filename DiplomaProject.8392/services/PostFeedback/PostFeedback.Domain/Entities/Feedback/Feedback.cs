
using DAL.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //T is either accommodation or user
    //accommodation/user is called "Item" in this class
    public class Feedback<T>: BaseEntity where T: FeedbackEntity
    {
        //id of accommodation or user for whom feedback is left
        public long ItemId { get; private set; }
        public T Item { get; private set; }
        //id of user who left the feedback
        public long? CreatorId { get; private set; }
        public User Creator { get; private set; }
        public int Rating { get; private set; }
        public string Message { get; private set; }
        //this constructor is called when new feedback is created
        public Feedback(
            long itemId,
            long? creatorId,
            int rating,
            string message)
        {
            ItemId = itemId;
            CreatorId = creatorId;
            Rating = rating;
            Message = message;
        }
        //this constructor is called when feedback is updated
        public Feedback(
           long id,
           long itemId,
           long? creatorId,
           int rating,
           string message)
            :base(id)
        {
            ItemId = itemId;
            CreatorId = creatorId;
            Rating = rating;
            Message = message;
        }
    }
}
