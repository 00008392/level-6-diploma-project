using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Profile.Domain.Logic.Helpers;
using System;
using System.Security.Cryptography;

namespace Profile.Helpers
{
    public class PasswordHandlingService : IPasswordHandlingService
    {

        public bool VerifyPassword(string password, string hash, string salt)
        {

            string hashedPassword = HashPassword(Convert.FromBase64String(salt), password);

            return hashedPassword == hash;
        }

        public string GetSalt()
        {
            var saltBytes = new byte[128 / 8];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);

        }
        public string HashPassword(byte[] saltBytes, string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        }
    }
}
