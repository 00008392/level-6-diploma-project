using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class AddItems: ErrorViewModel
    {
        public long AccommodationId { get; set; }
        public ICollection<PostItem> ItemsJson { get; set; }
    }
}
