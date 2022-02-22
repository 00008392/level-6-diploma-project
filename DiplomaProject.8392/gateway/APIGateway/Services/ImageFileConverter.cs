using APIGateway.Exceptions;
using APIGateway.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Services
{
    //service that converts image files into bytes
    public class ImageFileConverter : IFileConverter
    {
        public byte[] ConvertFile(IFormFile file)
        {
            //check that file is not empty
            if (file != null)
            {
                //check if file is image with jpg/jpeg/png format
                if(!IsImage(file))
                {
                    throw new FileContentTypeException();
                }
                //if correct format, convert to bytes
                byte[] photoBytes = null;
                using (var memory = new MemoryStream())
                {
                    file.CopyTo(memory);
                    photoBytes = memory.ToArray();
                }
                return photoBytes;
            }
            throw new EmptyFileException();
        }
        //check if file is image with jpg/jpeg/png format
        private bool IsImage(IFormFile file)
        {
            return string.Equals(file.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(file.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(file.ContentType, "image/png", StringComparison.OrdinalIgnoreCase);
        }
    }
}
