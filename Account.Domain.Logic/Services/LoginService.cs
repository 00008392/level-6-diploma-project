using Account.Domain.Logic.Interfaces;
using Account.Domain.Logic.DTOs;
using Account.Domain.Entities;
using Account.Domain.Core;
using Account.Domain.Logic.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Account.Domain.Logic.Services
{
    public class LoginService : BaseService, ILoginService
    {
        public LoginService(IRepository<User> repository, IPasswordHandlingService pwdService):base(repository, pwdService)
        {
        }
        public async Task<LoggedUserDTO> LoginUserAsync(UserLoginDTO login)
        {
            var userWithEmail = (await _repository.GetItemsAsync(u => u.Email.ToLower() == login.Email.ToLower())).FirstOrDefault();
            if(userWithEmail!=null)
            {
                if (_pwdService.VerifyPassword(login.Password, userWithEmail.PasswordHash, userWithEmail.PasswordSalt)) 
                {
                    return new LoggedUserDTO
                    {
                        Id = userWithEmail.Id,
                        Role = userWithEmail.Role,
                        Email = userWithEmail.Email
                    };
                }
                return null;
            }

            return null;
        }
    }
}
