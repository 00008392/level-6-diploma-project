using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Account.PasswordHandling
{
    public class PasswordHandlingService : IPasswordHandlingService
    {
        //to verify password, encrypt password provided for verification and compare its hash
        //with hash of original password
        public bool VerifyPassword(string password, string originalHash, string originalSsalt)
        {
            string hashedPassword = HashPassword(originalSsalt, password);
            return hashedPassword == originalHash;
        }
        //generate random salt
        public string GetSalt()
        {
            var saltBytes = new byte[128 / 8];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        //hash password using provided salt with HMACSHA1 algorithm
        public string HashPassword(string salt, string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        }
    }
}
