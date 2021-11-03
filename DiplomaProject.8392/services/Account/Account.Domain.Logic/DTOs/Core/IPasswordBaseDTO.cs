using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.DTOs.Core
{
    //need this base for fluent validation
   public interface IPasswordBaseDTO
    {
         string Password { get;  set; }
        
    }
}
