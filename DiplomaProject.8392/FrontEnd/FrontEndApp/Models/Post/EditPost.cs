using FrontEndApp.Models.Core;
using FrontEndApp.Models.Post.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Post
{
    public class EditPost: PostBase
    {
        [Required(ErrorMessage ="Moving in time is required")]
        public DateTime? MovingInTime { get; set; }
        [Required(ErrorMessage = "Moving out time is required")]
        public DateTime? MovingOutTime { get; set; }
        public ICollection<long> Rules { get; set; }
        public ICollection<long> Facilities { get; set; }

    }
}
