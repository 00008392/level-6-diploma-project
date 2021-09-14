using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
    public class AccommodationItemInfoDTO
    {
        public long Id { get; private set; }
        public string OtherItem { get; private set; }
        public ItemInfoDTO Item { get; private set; }

        public AccommodationItemInfoDTO(long id, string otherItem, ItemInfoDTO item)
        {
            Id = id;
            OtherItem = otherItem;
            Item = item;
        }
    }
}
