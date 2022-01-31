using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostFeedback.Domain.Logic.DTOs
{
    public class PhotoDTO
    {
        public long Id { get; private set; }
        public byte[] Photo { get; private set; }
        public string MimeType { get; private set; }

        public PhotoDTO(long id, byte[] photo, string mimeType)
        {
            Id = id;
            Photo = photo;
            MimeType = mimeType;
        }
    }
}
