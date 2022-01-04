using BaseClasses.Entities;
using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    //T is either accommodation or user
    public class Feedback<T>: BaseEntity where T: FeedbackEntity
    {
        //id of accommodation or user for whom feedback is left
        public long ItemId { get; private set; }
        public T Item { get; }
        //id of user who left the feedback
        public long? UserId { get; private set; }
        public User FeedbackOwner { get; }
        public int Rating { get; private set; }
        public string Message { get; private set; }

        public Feedback(
            long itemId,
            long? userId,
            int rating,
            string message)
        {
            ItemId = itemId;
            UserId = userId;
            Rating = rating;
            Message = message;
        }
        public Feedback(
           long id,
           long itemId,
           long? userId,
           int rating,
           string message):base(id)
        {
            ItemId = itemId;
            UserId = userId;
            Rating = rating;
            Message = message;
        }
    }
}
