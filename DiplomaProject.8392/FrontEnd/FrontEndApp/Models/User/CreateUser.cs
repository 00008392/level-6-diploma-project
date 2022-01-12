using FrontEndApp.Enums;
using FrontEndApp.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class CreateUser: UserBase
    {
        [Required]
        public string Password { get; set; }
    }
}
