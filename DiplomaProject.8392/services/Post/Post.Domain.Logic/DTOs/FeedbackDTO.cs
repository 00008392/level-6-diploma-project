using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
    public class FeedbackDTO
    {
        public long? UserId { get; private set; }
        public long ItemId { get; private set; }
        public int Rating { get; private set; }
        public string Message { get; private set; }

        public FeedbackDTO(long? userId, long itemId, int rating, string message)
        {
            UserId = userId;
            ItemId = itemId;
            Rating = rating;
            Message = message;
        }
    }
}
