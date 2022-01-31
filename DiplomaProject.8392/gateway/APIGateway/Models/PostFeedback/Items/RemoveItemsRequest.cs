using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    public partial class RemoveItemsRequest : IPostItemsRequest<long>
    {
        public ICollection<long> ItemsJson { get ; set ; }

        
    }
}
