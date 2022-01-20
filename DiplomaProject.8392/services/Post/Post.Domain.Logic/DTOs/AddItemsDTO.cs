using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
   public class AddItemsDTO
    {
        public long AccommodationId { get; private set; }
        public ICollection<AccommodationItemDTO> Items { get; private set; }

        public AddItemsDTO(long accommodationId, ICollection<AccommodationItemDTO> items)
        {
            AccommodationId = accommodationId;
            Items = items;
        }
    }
}
