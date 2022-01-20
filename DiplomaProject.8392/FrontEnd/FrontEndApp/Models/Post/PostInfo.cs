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
        [JsonProperty("AccommodationRules")]
        public ICollection<PostItemInfo> Rules { get; set; }
        [JsonProperty("AccommodationSpecificities")]
        public ICollection<PostItemInfo> Specificities { get; set; }
        [JsonProperty("AccommodationFacilities")]
        public ICollection<PostItemInfo> Facilities { get; set; }
    }
}
