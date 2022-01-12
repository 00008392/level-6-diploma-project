using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class ChangePassword: ErrorViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
