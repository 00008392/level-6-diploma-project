﻿using Account.Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Account.Services
{
    public class PasswordHandlingService : IPasswordHandlingService
    {
        public string CreatePasswordHash(string password)
        {
            byte[] saltBytes = Convert.FromBase64String(GetSalt());
            return HashPassword(saltBytes, password);

        }

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
        private string HashPassword(byte[] saltBytes, string password)
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
