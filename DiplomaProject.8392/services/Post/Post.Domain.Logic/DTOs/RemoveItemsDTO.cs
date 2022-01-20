using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.DTOs
{
    public class RemoveItemsDTO
    {
        public long AccommodationId { get; private set; }
        public ICollection<long> Items { get; private set; }

        public RemoveItemsDTO(long accommodationId, ICollection<long> items)
        {
            AccommodationId = accommodationId;
            Items = items;
        }
    }
}
