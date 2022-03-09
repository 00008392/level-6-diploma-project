using FrontEndApp.Models.User;
using FrontEndApp.Services.Core;
using FrontEndApp.Services.User.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEndApp.Services.User
{
    //service that consumes user related information api
    public class UserRelatedInfoService : BaseService, IUserRelatedInfoService
    {
        public UserRelatedInfoService(HttpClient client):base(client)
        {
        }
        //get all countries
        public async Task<ICollection<Country>> GetCountriesAsync()
        {
            //call base service method for multiple items retrieval
            return await HandleMultipleItemsRetrievalAsync<Country>
                ("api/users/info/countries");
        }
    }
}
