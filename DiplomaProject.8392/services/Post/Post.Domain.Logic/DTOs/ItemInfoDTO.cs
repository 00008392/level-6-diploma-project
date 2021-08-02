using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
   public class ItemInfoDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool? IsOther { get; set; }
    }
}
