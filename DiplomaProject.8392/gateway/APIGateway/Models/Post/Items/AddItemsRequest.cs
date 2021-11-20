using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API
{
    public partial class AddItemsRequest : IAccommodationItemsRequest<ItemRequest>
    {
        public ICollection<ItemRequest> ItemsJson { get ; set; }
        
    }
}
