using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; }
        public Error[] Errors { get; set; }
        public string Message { get; set; }
    }
}
