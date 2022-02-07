using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    public partial class AddItemsRequest : IPostItemsRequest<ItemRequest>
    {
        public ICollection<ItemRequest> ItemsJson { get ; set; }
        
    }
}
