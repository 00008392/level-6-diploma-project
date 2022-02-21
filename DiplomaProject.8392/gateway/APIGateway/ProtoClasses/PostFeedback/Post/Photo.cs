using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    public partial class Photo: IPhoto
    {
        public byte[] FileContent { get; set; }
    }
}
