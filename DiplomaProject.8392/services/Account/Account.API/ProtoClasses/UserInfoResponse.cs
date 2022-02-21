using Grpc.Base.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API
{
    //need to implement this interface in order to call helper method that handles retrieval of item by id
    public partial class UserInfoResponse: IItemInfoResponse
    {
    }
}
