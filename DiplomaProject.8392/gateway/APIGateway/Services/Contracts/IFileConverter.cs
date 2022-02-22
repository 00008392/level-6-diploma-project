using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Services.Contracts
{
    //service that converts files into bytes
    public interface IFileConverter
    {
        byte[] ConvertFile(IFormFile file);
    }
}
