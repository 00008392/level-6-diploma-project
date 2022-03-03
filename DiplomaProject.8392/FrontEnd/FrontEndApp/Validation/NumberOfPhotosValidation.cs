using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Validation
{
    public class NumberOfPhotosValidation: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var files = value as ICollection<IBrowserFile>;
            return files?.Count <= 15;
        }
    }
}
