using FrontEndApp.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Services.User.Contracts
{
    public interface IUserRelatedInfoService
    {
        Task<ICollection<Country>> GetCountriesAsync();
    }
}
