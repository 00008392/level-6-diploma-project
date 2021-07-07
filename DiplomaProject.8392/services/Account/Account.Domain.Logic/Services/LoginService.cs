using Account.DAL.EF.Data;
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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Services
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly AccountDbContext _context;
        public LoginService(IRepository<User> repository, IPasswordHandlingService pwdService,
                            AccountDbContext context) : base(repository, pwdService)
        {
            _context = context;
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
