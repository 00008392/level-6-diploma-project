using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class PostInfo: PostBase
    {
        public string MovingInTime { get; set; }
        public string MovingOutTime { get; set; }
        public DateTime DatePublished { get; set; }
        [JsonProperty("Rules")]
        public List<PostItem> Rules { get; set; }
        [JsonProperty("Specificities")]
        public List<PostItem> Specificities { get; set; }
        [JsonProperty("Facilities")]
        public List<PostItem> Facilities { get; set; }
    }
}
