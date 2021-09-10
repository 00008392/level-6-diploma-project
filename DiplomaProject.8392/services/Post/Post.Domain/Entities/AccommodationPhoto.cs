using Post.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Entities
{
    public class AccommodationPhoto: AccommodationEntityBase
    {

        public byte[] Photo { get; private set; }
        public string MimeType { get; private set; }
        public AccommodationPhoto(byte[] photo, string mimeType, long accommodationId) 
            : base(accommodationId)
        {
            Photo = photo;
            MimeType = mimeType;
        }

    }
}
