using Blazored.SessionStorage;
using FrontEndApp.Models.User;
using FrontEndApp.Services.Authentication.Contracts;
using FrontEndApp.Services.Core;
using FrontEndApp.States;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontEndApp.Services.Authentication
{
    //service that consumes authentication api
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private ISessionStorageService _storage;
        private LoggedInState _authState;

        public AuthenticationService(
            ISessionStorageService storage,
            HttpClient client,
            LoggedInState authState) 
            :base(client)
        {
            _storage = storage;
            _authState = authState;
        }
        //get id of user that is logged in
        public async Task<long?> GetLoggedUserIdAsync()
        {
            //check if user is authenticated
            if (await IsAuthenticatedAsync())
            {
                //parse id
                return long.Parse(await _storage.GetItemAsync<string>("userId"));
            }
            return null;
        }
        //check is user is authenticated
        public async Task<bool> IsAuthenticatedAsync()
        {
            //if there is token in session storage, user is authenticated
            var token = await _storage.GetItemAsync<string>("JWTToken");
            return token != null;
        }
        //log in
        public async Task<LoginResponse> LogInAsync(LoginRequest login, Action onLoginAction = null, Action onErrorAction = null)
        {
            try
            {
                //call api
                var reply = await _client.PostAsJsonAsync("api/login", login);
                LoginResponse user = null;
                if (reply.IsSuccessStatusCode)
                {
                    //in case if success, parse response and put JWT,
                    //email and id of user to session storage
                    var responseStr = await reply.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<LoginResponse>(responseStr);
                    await _storage.SetItemAsync("JWTToken", user.JWTToken);
                    await _storage.SetItemAsync("userEmail", user.Email);
                    await _storage.SetItemAsync("userId", user.Id);
                    //add JWT to headers by default
                    _client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("bearer", user.JWTToken);
                    //set auth state to authenticated
                    _authState.isAuthenticated = true;
                    //invoke action that reacts to login
                    onLoginAction?.Invoke();
                } else
                {
                    //invoke action that reacts when login fails
                    onErrorAction?.Invoke();
                }
                return user;
            }
            catch
            {
                //in case of exception return null
                return null;
            }

        }
        //log out
        public async Task LogOutAsync(Action onLogoutAction = null)
        {
            //remove JWT, email and id of user from session storage
            await _storage.RemoveItemAsync("JWTToken");
            await _storage.RemoveItemAsync("userEmail");
            await _storage.RemoveItemAsync("userId");
            //clear default headers
            _client.DefaultRequestHeaders.Clear();
            //set auth state to not authenticated
            _authState.isAuthenticated = false;
            //invoke action that reacts to logout
            onLogoutAction?.Invoke();
        }
    }
}
