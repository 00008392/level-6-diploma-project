using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API
{
    public interface IAccommodationItemsRequest<T>
    {
         ICollection<T> ItemsJson { get; set; }
         RepeatedField<T> Items { get; }
    }
}
