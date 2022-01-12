using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post.API
{
    public interface IItemList<T>
    {
        RepeatedField<T> Items { get; }
    }
}
