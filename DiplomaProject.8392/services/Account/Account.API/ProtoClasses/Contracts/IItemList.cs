using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API
{
    //interface defined for method that maps collection of dtos to grpc generated objects and adds it to grpc reponse
    public interface IItemList<T>
    {
        RepeatedField<T> Items { get; }
    }
}
