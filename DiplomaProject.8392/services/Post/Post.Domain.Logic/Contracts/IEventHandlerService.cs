using Post.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Logic.Contracts
{
    public interface IEventHandlerService
    {
        Task CreateUserAsync(CreateUserDTO user);
        Task DeleteUserAsync(long id);
        Task UpdateUserAsync(UpdateUserDTO user);
    }
}
