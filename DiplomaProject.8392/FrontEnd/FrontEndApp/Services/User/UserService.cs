using FrontEndApp.Models;
using FrontEndApp.Models.User;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FrontEndApp.Services.User.Contracts;
using FrontEndApp.Services.Authentication;
using FrontEndApp.Services.Core;

namespace FrontEndApp.Services.User
{
    //service that consumes user api
    public class UserService : BaseService, IUserService
    {
        public UserService(
            HttpClient client)
            :base(client)
        {
        }
        //update password
        public async Task<Response> ChangePasswordAsync(ChangePassword request, Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            //call base service method for update
            return await HandleUpdateActionAsync(request, "api/users/account/password", onSuccessAction,
                onErrorAction);
        }
        //delete account
        public async Task<Response> DeleteAccountAsync(Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            //call base service method for delete
            return await HandleDeleteActionAsync("api/users/account", onSuccessAction, onErrorAction);
        }
        //get user by id
        public async Task<UserResponse> GetUserAsync(long id, Action onNotFoundAction = null)
        {
            //call base service method for single item retrieval
            return await HandleSingleItemRetrievalAsync<UserResponse>($"api/users/{id}", onNotFoundAction);
        }
        //get all users
        public async Task<ICollection<UserResponse>> GetUsersAsync()
        {
            //call base service method for multiple items retrieval
            return await HandleMultipleItemsRetrievalAsync<UserResponse>("api/users");
        }
        //register new user
        public async Task<Response> RegisterAccountAsync(RegisterUser request, Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            //call base service method for create
            return await HandleCreateActionAsync(request, "api/users", onSuccessAction, onErrorAction);
        }
        //update existing user
        public async Task<Response> UpdateAccountAsync(UpdateUser request, Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            //call base service method for update
            return await HandleUpdateActionAsync(request, "api/users/account", onSuccessAction, onErrorAction);
        }
    }
}
