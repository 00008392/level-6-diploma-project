using Account.Domain.Entities;
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.DTOs;
using AutoMapper;
using BaseClasses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Services
{
    //service for retrieving entities related to user and necessary for user creation and modification
    //in this case, such entity is country
    public class UserRelatedInfoService : IUserRelatedInfoService
    {
        private readonly IRepository<Country> _repository;
        private readonly IMapper _mapper;

        public UserRelatedInfoService(
            IRepository<Country> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<CountryDTO>> GetAllCountriesAsync()
        {
            //get all countries from the database
            var countries = (await _repository.GetAllAsync()).ToList();
            //map list of country domai entities to list of DTOs
            return _mapper.Map<ICollection<Country>, ICollection<CountryDTO>>(countries);
        }
    }
}
