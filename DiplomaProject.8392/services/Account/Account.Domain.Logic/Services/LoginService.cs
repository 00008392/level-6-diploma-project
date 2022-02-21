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
using DAL.Base.Contracts;
using AutoMapper;

namespace Account.Domain.Logic.Services
{
    //user login service
    public class LoginService : BaseService, ILoginService
    {
        public LoginService(
            IRepository<User> repository,
            IPasswordHandlingService pwdService,
            IMapper mapper) 
            : base(
                  repository,
                  mapper,
                  pwdService)
        {
        }
        //method for logging in
        public async Task<LoggedUserDTO> LoginUserAsync(UserLoginDTO login)
        {
            //get user with indicated email
            var user = (await _repository.GetFilteredAsync(user => user.Email == login.Email)).FirstOrDefault();
            if (user != null)
            {
                //if user with email exists, verify password
                if (_pwdService.VerifyPassword(login.Password, user.PasswordHash, user.PasswordSalt))
                {
                    //if password is correct, map user to dto and return
                    return _mapper.Map<LoggedUserDTO>(user);
                }
                
                return null;
            }

            return null;
        }
    }
}
