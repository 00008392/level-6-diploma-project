using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.PasswordHandling
{
    public interface IPasswordHandlingService
    {
        string HashPassword(byte[] saltBytes, string password);
        bool VerifyPassword(string password, string hash, string salt);
        string GetSalt();
    }
}
