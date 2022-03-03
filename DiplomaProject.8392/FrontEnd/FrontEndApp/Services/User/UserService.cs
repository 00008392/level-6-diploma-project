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

namespace FrontEndApp.Services.User
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;
        public UserService(
            HttpClient client)
        {
            _client = client;
        }

        public async Task<Response> ChangePasswordAsync(ChangePassword request, Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                //call api
                var httpReply = await _client.PutAsJsonAsync
            ($"api/users/account/password", request);
                if (httpReply.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    onSuccessAction?.Invoke();
                }
                else
                {
                    //in case of error, parse error 
                    response.IsSuccess = false;
                    var errorMessage = httpReply.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response>(errorMessage);
                    onErrorAction?.Invoke();
                }
            } 
            catch
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<Response> DeleteAccountAsync(Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                //call api
                var httpReply = await _client.DeleteAsync($"api/users/account");
                //in case of success, log out
                if (httpReply.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    onSuccessAction?.Invoke();
                }
                else
                {
                    //in case of error, parse error 
                    response.IsSuccess = false;
                    var errorMessage = httpReply.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response>(errorMessage);
                    onErrorAction?.Invoke();
                }
            }
            catch
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<UserResponse> GetUserAsync(long id, Action onNotFoundAction = null)
        {
            try
            {
                var reply = await _client.GetAsync($"api/users/{id}");
                var user = new UserResponse();
                if (reply.IsSuccessStatusCode)
                {
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<UserResponse>(responseStr);
                } else
                {
                    user.NoItem = true;
                    onNotFoundAction?.Invoke();
                }
                return user;
            } catch
            {
                return null;
            }
        }

        public async Task<ICollection<UserResponse>> GetUsersAsync()
        {
            //call api
            try
            {
                var response = await _client.GetAsync("api/users");
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<UserResponse>>(responseStr);
                }
            }
            catch
            {
            }
            return null;
        }

        public async Task<Response> RegisterAccountAsync(RegisterUser request, Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                //call api
                var httpReply = await _client.PostAsJsonAsync("api/users", request);
                //in case of success, login and set response to success
                if (httpReply.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    onSuccessAction?.Invoke();
                }
                //in case of error, parse error 
                else
                {
                    response.IsSuccess = false;
                    var errorMessage = httpReply.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response>(errorMessage);
                    onErrorAction?.Invoke();
                }
            } 
            catch
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<Response> UpdateAccountAsync(UpdateUser request, Action onSuccessAction = null,
            Action onErrorAction = null)
        {
            var response = new Response();
            try
            {
                //call api
                var httpReply = await _client.PutAsJsonAsync($"api/users/account", request);
                if (httpReply.IsSuccessStatusCode)
                {
                    response.IsSuccess = true;
                    onSuccessAction?.Invoke();
                }
                //in case of error, parse error 
                else
                {
                    response.IsSuccess = false;
                    var errorMessage = httpReply.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<Response>(errorMessage);
                    onErrorAction?.Invoke();
                }
            } 
            catch
            {
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
