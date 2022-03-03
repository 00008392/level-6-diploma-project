using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Validation
{
    public abstract class PhotoBaseValidation: ValidationAttribute
    {
        public bool IsValidSize(IBrowserFile file)
        {
            return file.Size <= 1024 * 1024;
        }
        public bool IsValidExtension(IBrowserFile file)
        {
            return string.Equals(file.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(file.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(file.ContentType, "image/png", StringComparison.OrdinalIgnoreCase);
        }
        public bool IsValidCollection(object value, Func<IBrowserFile, bool> isValidAction)
        {
            var files = value as ICollection<IBrowserFile>;
            var isValid = true;
            foreach (var file in files)
            {
                if (!isValidAction(file))
                {
                    isValid = false;
                }
            }
            return isValid;
        }
    }
}
