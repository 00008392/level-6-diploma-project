
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
        public bool? IsCover { get; private set; }
        public Photo(
            long postId,
            byte[] photoBytes,
            bool? isCover) 
        {
            PostId = postId;
            PhotoBytes = photoBytes;
            IsCover = isCover;
        }

    }
}
