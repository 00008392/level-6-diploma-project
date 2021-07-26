using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Core
{
    //DTO fir bridge tables between accommodation and rules/facilities/specificities
    public abstract class AccommodationItemDTO
    {
        public long AccommodationId  { get; set; }
        public long ItemId  { get; set; }
        public string OtherItem  { get; set; }
    }
}
