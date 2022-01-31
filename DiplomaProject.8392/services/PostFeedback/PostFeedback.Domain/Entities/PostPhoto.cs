
using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Entities
{
    public class PostPhoto: BaseEntity
    {
        public long PostId { get; protected set; }
        public Post Post { get; }
        public byte[] Photo { get; private set; }
        public string MimeType { get; private set; }
        public PostPhoto(byte[] photo, string mimeType, long postId) 
            : base(postId)
        {
            Photo = photo;
            MimeType = mimeType;
        }

    }
}
