using FrontEndApp.Validation;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Post
{
    public class UploadMultiplePhotos
    {
        [Required(ErrorMessage ="No files submitted")]
        [NumberOfPhotosValidation(ErrorMessage ="Limit of 15 photos is exceeded")]
        [FileTypeValidationMultiplePhotos(ErrorMessage ="Invalid file type")]
        [FileSizeValidationMultiplePhotos(ErrorMessage ="Maximum file size of 1MB is exceeded")]
        public ICollection<IBrowserFile> Files { get; set; }
    }
}
