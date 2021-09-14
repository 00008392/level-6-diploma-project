
using Account.Domain.Enums;
using BaseClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Entities
{
   public class User: BaseEntity
    {
        public string Email { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public Role Role { get; private set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }

        public User(string email, DateTime registrationDate,
            Role role, string passwordHash, string passwordSalt)
        {
            Email = email;
            RegistrationDate = registrationDate;
            Role = role;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
        public void ChangeEmail(string email)
        {
            Email = email;
        }
        public void ChangePassword(string passwordHash, string passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
