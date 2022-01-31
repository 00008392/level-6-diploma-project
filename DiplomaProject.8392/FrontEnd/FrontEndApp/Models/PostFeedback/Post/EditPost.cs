using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class EditPost: PostBase
    {
        [Required]
        public DateTime? MovingInTime { get;  set; }
        [Required]
        public DateTime? MovingOutTime { get;  set; }
        public List<Item> Cities { get; set; }
        public List<Item> Categories { get; set; }
    }
}
