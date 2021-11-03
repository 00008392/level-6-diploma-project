using Account.DAL.EF.Data;
using Account.Domain.Entities;
using Account.Domain.Enums;
using Account.Domain.Logic.Services.Core;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.Contracts;
using Account.PasswordHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BaseClasses.Contracts;
using AutoMapper;

namespace Account.Domain.Logic.Services
{
    public class LoginService : BasePasswordService, ILoginService
    {
        public LoginService(IRepositoryWithIncludes<User> repository,
            IPasswordHandlingService pwdService,
            IMapper mapper) : base(repository, pwdService, mapper)
        {
        }
        public async Task<LoggedUserDTO> LoginUserAsync(UserLoginDTO login)
        {

            var usersWithEmail = await _repository.GetFilteredAsync(user => user.Email == login.Email);
            var user = usersWithEmail.FirstOrDefault();
            if (user != null)
            {
                if (_pwdService.VerifyPassword(login.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return _mapper.Map<LoggedUserDTO>(user);
                }
                
                return null;
            }

            return null;
        }
    }
}
