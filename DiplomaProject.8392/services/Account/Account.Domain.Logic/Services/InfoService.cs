using Account.Domain.Entities;
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.Services.Core;
using AutoMapper;
using BaseClasses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Services
{
    public class InfoService : BaseService, IInfoService
    {
        private readonly IRepository<Country> _countryRepository;
        public InfoService(IRepositoryWithIncludes<User> repository, 
            IMapper mapper, IRepository<Country> countryRepository):base(repository, mapper)
        {
            _countryRepository = countryRepository;
        }

        public async Task<ICollection<CountryDTO>> GetAllCountriesAsync()
        {
            var countries = (await _countryRepository.GetAllAsync()).ToList();
            return _mapper.Map<ICollection<Country>, ICollection<CountryDTO>>(countries);
        }

        public async Task<ICollection<UserInfoDTO>> GetAllUsersAsync()
        {
            var users = (await _repository.GetAllAsync(relatedEntitiesIncluded: true)).ToList();
            return _mapper.Map<ICollection<User>, ICollection<UserInfoDTO>>(users);
        }

        public async Task<UserInfoDTO> GetProfileInfoAsync(long id)
        {
            var user = await _repository.GetByIdAsync(id, true);
            if (user != null)
            {
                return _mapper.Map<UserInfoDTO>(user);
            }

            return null;
        }

    }
}
