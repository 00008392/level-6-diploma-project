using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Validation
{
    public class FileSizeValidationSinglePhoto: PhotoBaseValidation
    {
        public override bool IsValid(object value)
        {
            return IsValidSize(value as IBrowserFile);
        }
    }
}
