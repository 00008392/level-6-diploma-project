using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class Photo
    {
        public byte[] FileContent { get; set; }
        public string MimeType { get; set; }
    }
}
