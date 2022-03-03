using FrontEndApp.Validation;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Post
{
    public class UploadSinglePhoto
    {
        [Required(ErrorMessage ="No file submitted")]
        [FileSizeValidationSinglePhoto(ErrorMessage ="Maximum file size of 1MB is exceeded")]
        [FileTypeValidationSinglePhoto(ErrorMessage ="Invalid image format")]
        public IBrowserFile File { get; set; }
    }
}
