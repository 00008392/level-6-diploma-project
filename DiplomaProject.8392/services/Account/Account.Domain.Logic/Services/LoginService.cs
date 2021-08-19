using Account.DAL.EF.Data;
using Account.Domain.Entities;
using Account.Domain.Enums;
using Account.Domain.Logic.Core;
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

namespace Account.Domain.Logic.Services
{
    public class LoginService : BaseService, ILoginService
    {
        public LoginService(IRepository<User> repository, IPasswordHandlingService pwdService
                           ) : base(repository, pwdService)
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
                    return new LoggedUserDTO
                    {
                        Id = user.Id,
                        Role = (int)user.Role,
                        Email = user.Email
                    };
                }
                
                return null;
            }

            return null;
        }
    }
}
