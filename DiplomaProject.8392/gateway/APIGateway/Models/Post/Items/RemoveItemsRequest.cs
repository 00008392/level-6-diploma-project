using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API
{
    public partial class RemoveItemsRequest : IAccommodationItemsRequest<long>
    {
        public ICollection<long> ItemsJson { get ; set ; }

        
    }
}
