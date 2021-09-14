
using Profile.Domain.Entities;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseClasses.Contracts;

namespace Profile.Domain.Logic.Services
{
    public class ProfileInfoService : IProfileInfoService
    {
        private readonly IRepository<User> _repository;
        private readonly IRepository<Country> _countryRepository;
        public ProfileInfoService(IRepository<User> repository,
            IRepository<Country> countryRepository)
        {
            _repository = repository;
            _countryRepository = countryRepository;
        }
        public async Task<ProfileInfoDTO> GetProfileInfoAsync(long id)
        {
            var user = await _repository.GetByIdAsync(id, u=>u.City);
            if(user!=null)
            {
                CityDTO cityDTO = null;
                CountryDTO countryDTO = null;
                if(user.City!=null)
                {
                    cityDTO = new CityDTO(user.City.Id, user.City.Name);
                    var country = await _countryRepository.GetByIdAsync(user.City.Id);
                    countryDTO = new CountryDTO(country.Id, country.Name);
                }
                
                var profileDTO = new ProfileInfoDTO(user.Id, user.FirstName, user.LastName,
                    user.Email, user.PhoneNumber, user.DateOfBirth, user.Gender,
                    user.Address, user.UserInfo, user.RegistrationDate, cityDTO,
                    countryDTO,
                    user.ProfilePhoto, user.MimeType);
               
                return profileDTO;
            }

            return null;
        }
    }
}
