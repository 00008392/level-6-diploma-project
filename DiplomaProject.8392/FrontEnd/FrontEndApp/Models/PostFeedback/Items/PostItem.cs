using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class PostItem
    {
        public long ItemId { get; set; }
        public string OtherValue { get; set; }
        public string Item { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
