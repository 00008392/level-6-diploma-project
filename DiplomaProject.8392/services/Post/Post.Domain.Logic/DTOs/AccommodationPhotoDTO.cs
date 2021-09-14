using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
    public class AccommodationPhotoDTO
    {
        public long Id { get; private set; }
        public byte[] Photo { get; private set; }
        public string MimeType { get; private set; }

        public AccommodationPhotoDTO(long id, byte[] photo, string mimeType)
        {
            Id = id;
            Photo = photo;
            MimeType = mimeType;
        }
    }
}
