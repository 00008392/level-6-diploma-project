using Blazored.SessionStorage;
using FrontEndApp.Models;
using FrontEndApp.States;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontEndApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private ISessionStorageService _storage;
        private HttpClient _client;
        private LoggedInState _authState;

        public AuthenticationService(ISessionStorageService storage, HttpClient client,
            LoggedInState authState)
        {
            _storage = storage;
            _client = client;
            _authState = authState;
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await _storage.GetItemAsync<string>("JWTToken");
            return token != null;
        }

        public async Task<LoggedUser> LogIn(Login login)
        {
            var reply = await _client.PostAsJsonAsync<Models.Login>("api/login", login);
            LoggedUser user = null;
            if (reply.IsSuccessStatusCode)
            {
                var responseStr = await reply.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<LoggedUser>(responseStr);
                await _storage.SetItemAsync("JWTToken", user.JWTToken);
                await _storage.SetItemAsync("userEmail", user.Email);
                await _storage.SetItemAsync("userId", user.Id);
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("bearer", user.JWTToken);
                _authState.isAuthenticated = true;
            }
            return user;
        }

        public async Task LogOut()
        {
            await _storage.RemoveItemAsync("JWTToken");
            await _storage.RemoveItemAsync("userEmail");
            await _storage.RemoveItemAsync("userId");
            _client.DefaultRequestHeaders.Clear();
            _authState.isAuthenticated = false;
        }
    }
}
