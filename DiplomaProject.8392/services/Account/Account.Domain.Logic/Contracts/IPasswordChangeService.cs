using Account.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Contracts
{
    public interface IPasswordChangeService
    {
        Task ChangePasswordAsync(ChangePasswordDTO password);
    }
}
