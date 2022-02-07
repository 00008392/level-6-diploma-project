using Account.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Contracts
{
    //service for retrieving entities related to user and necessary for user creation and modification
    //in this case, such entity is country
    public interface IUserRelatedInfoService
    {
        Task<ICollection<CountryDTO>> GetAllCountriesAsync();
    }
}
