using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.PasswordHandling
{
    //this service is used by account microservice to encrypt password and verify it when user logs in
    public interface IPasswordHandlingService
    {
        string HashPassword(string salt, string password);
        bool VerifyPassword(string password, string originalHash, string originalSalt);
        string GetSalt();
    }
}
