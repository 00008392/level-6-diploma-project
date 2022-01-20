using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class PostItemInfo
    {
        public long Id { get; set; }
        public string OtherValue { get; set; }
        [JsonProperty("Base")]
        public Item Item { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
