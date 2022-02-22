
using DAL.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    //photo of accommodation
    public class Photo: BaseEntity
    {
        public long PostId { get; private set; }
        public Post Post { get; private set; }
        public byte[] PhotoBytes { get; private set; }
        public Photo(
            long postId,
            byte[] photoBytes) 
        {
            PostId = postId;
            PhotoBytes = photoBytes;
        }

    }
}
