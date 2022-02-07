using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API
{
    //interface implemented by user update and password change classes
    //necessary for resource based authorization
    //both update and password change have the same authorization requirement
    public interface IUpdateRequest
    {
        long Id { get; set; }
    }
}
