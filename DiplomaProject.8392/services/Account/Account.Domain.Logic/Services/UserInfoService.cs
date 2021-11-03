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
    public class UserInfoService : BaseService, IUserInfoService
    {
        public UserInfoService(IRepositoryWithIncludes<User> repository, 
            IMapper mapper):base(repository, mapper)
        {

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
