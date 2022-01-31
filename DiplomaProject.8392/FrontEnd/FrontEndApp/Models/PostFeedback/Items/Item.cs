using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class Item
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; } = false;
        public bool isOther { get; set; }
        public string OtherValue { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
