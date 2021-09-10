using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
    //DTO for bridge tables between accommodation and rules/facilities/specificities
    public class AccommodationItemDTO
    {
        public long AccommodationId  { get; private set; }
        public long ItemId  { get; private set; }
        public string OtherItem  { get; private set; }

        public AccommodationItemDTO(long accommodationId, long itemId, string otherItem)
        {
            AccommodationId = accommodationId;
            ItemId = itemId;
            OtherItem = otherItem;
        }
    }
}
