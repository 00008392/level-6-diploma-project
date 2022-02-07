using Account.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Contracts
{
    //user CRUD service
    public interface IUserService
    {
        Task RegisterUserAsync(UserRegistrationDTO user);
        Task UpdateUserAsync(UserUpdateDTO user);
        Task DeleteUserAsync(long id);
        Task ChangePasswordAsync(ChangePasswordDTO password);
        Task<UserInfoDTO> GetUserInfoAsync(long id);
        Task<ICollection<UserInfoDTO>> GetAllUsersAsync();
    }
}
