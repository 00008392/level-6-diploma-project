using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class RemoveItems: ErrorViewModel
    {
        public long PostId { get; set; }
        public ICollection<long> ItemsJson { get; set; }
    }
}
