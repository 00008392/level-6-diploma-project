using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Logic.Helpers
{
    public interface IPasswordHandlingService
    {
        string CreatePasswordHash(string password);
        bool VerifyPassword(string password, string hash, string salt);
        string GetSalt();
    }
}
