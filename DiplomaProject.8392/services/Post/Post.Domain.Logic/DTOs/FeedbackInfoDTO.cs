using Post.Domain.Logic.DTOs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
   public class FeedbackInfoDTO<T>: FeedbackDTO where T:IFeedbackEntityDTO
    {
        public long Id { get; private set; }
        public T Item { get; private set; }
        public UserDTO FeedbackOwner { get; private set; }
        public FeedbackInfoDTO(
             long? userId,
             long itemId,
             int rating,
             string message,
             long id,
             T item,
             UserDTO feedbackOwner) :
             base(userId, itemId, rating, message)
        {
            Id = id;
            Item = item;
            FeedbackOwner = feedbackOwner;
        }
    }
}
