using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Post
{
    public class Photo
    {
        public long Id { get; set; }
        public byte[] FileContent { get; set; }
        public string FileContentStr { get; set; }
        public long PostId { get; set; }
    }
}
