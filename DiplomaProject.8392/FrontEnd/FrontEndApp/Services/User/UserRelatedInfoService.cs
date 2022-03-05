using FrontEndApp.Models.User;
using FrontEndApp.Services.User.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEndApp.Services.User
{
    public class UserRelatedInfoService : IUserRelatedInfoService
    {
        private readonly HttpClient _client;

        public UserRelatedInfoService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ICollection<Country>> GetCountriesAsync()
        {
            try
            {
                var response = await _client.GetAsync("api/users/info/countries");
                if (response.IsSuccessStatusCode)
                {
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var countries = JsonConvert.DeserializeObject<List<Country>>(responseStr);
                    return countries?.Count == 0 ? null : countries;
                }
            } catch
            {

            }
            return null;
        }
    }
}
