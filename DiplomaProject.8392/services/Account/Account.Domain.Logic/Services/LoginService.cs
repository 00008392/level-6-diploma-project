using Account.Domain.Core;
using Account.Domain.Entities;
using Account.Domain.Enums;
using Account.Domain.Logic.Core;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.Helpers;
using Account.Domain.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Services
{
    public class LoginService : BaseService, ILoginService
    {
        public LoginService(IRepository<User> repository, IPasswordHandlingService pwdService) : base(repository, pwdService)
        {
        }
        public async Task<LoggedUserDTO> LoginUserAsync(UserLoginDTO login)
        {
            var userWithEmail = (await _repository.GetFilteredAsync(u => u.Email.ToLower() == login.Email.ToLower())).FirstOrDefault();
            if (userWithEmail != null)
            {
                if (_pwdService.VerifyPassword(login.Password, userWithEmail.PasswordHash, userWithEmail.PasswordSalt))
                {
                    return new LoggedUserDTO
                    {
                        Id = userWithEmail.Id,
                        Role = (int)userWithEmail.Role,
                        Email = userWithEmail.Email
                    };
                }
                
                return null;
            }

            return null;
        }
    }
}
