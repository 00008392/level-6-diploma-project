using FrontEndApp.Models;
using FrontEndApp.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEndApp.Services.User.Contracts
{
    public interface IUserService
    {
        Task<Response> RegisterAccountAsync(RegisterUser request, Action onSuccessAction = null,
            Action onErrorAction = null);
        Task<Response> UpdateAccountAsync(UpdateUser request, Action onSuccessAction = null,
            Action onErrorAction = null);
        Task<Response> DeleteAccountAsync(Action onSuccessAction = null,
            Action onErrorAction = null);
        Task<Response> ChangePasswordAsync(ChangePassword request, Action onSuccessAction = null,
            Action onErrorAction = null);
        Task<UserResponse> GetUserAsync(long id, Action onNotFoundAction = null);
        Task<ICollection<UserResponse>> GetUsersAsync();
    }
}
