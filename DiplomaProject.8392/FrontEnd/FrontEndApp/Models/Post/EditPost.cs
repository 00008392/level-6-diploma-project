using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class EditPost: PostBase
    {
        public long OwnerId { get;  set; }
        public long? CategoryId { get;  set; }
        public long? CityId { get;  set; }
        [Required]
        public DateTime? MovingInTime { get;  set; }
        [Required]
        public DateTime? MovingOutTime { get;  set; }
        public ICollection<Item> Cities { get; set; }
        public ICollection<Item> Categories { get; set; }
    }
}
