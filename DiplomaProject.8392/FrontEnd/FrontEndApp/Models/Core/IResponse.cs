using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models.Core
{
    //interface implemented by responses to be used in common methods
    public interface IResponse
    {
        bool NoItem { get; set; }
    }
}
