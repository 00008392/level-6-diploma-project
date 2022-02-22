using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    //IPhoto - in order to hide properties from user indicated in the interface
    public partial class Photo: IPhoto
    {
        //return photo as byte array
        public byte[] FileContent { get; set; }
    }
}
