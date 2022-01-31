using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostFeedback.API
{
    public interface IPostItemsRequest<T>
    {
         ICollection<T> ItemsJson { get; set; }
         RepeatedField<T> Items { get; }
    }
}
