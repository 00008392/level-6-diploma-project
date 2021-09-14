
using Profile.Domain.Entities;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseClasses.Contracts;
using AutoMapper;
using Profile.Domain.Logic.Services.Core;

namespace Profile.Domain.Logic.Services
{
    public class ProfileInfoService : BaseService, IProfileInfoService
    {
        private readonly IMapper _mapper;
        public ProfileInfoService(IRepository<User> repository,
           IMapper mapper):base(repository)
        {
            _mapper = mapper;
        }
        public async Task<ProfileInfoDTO> GetProfileInfoAsync(long id)
        {
            var user = await _repository.GetByIdAsync(id, u=>u.City);
            if(user!=null)
            {

                return _mapper.Map<ProfileInfoDTO>(user);
            }

            return null;
        }
    }
}
