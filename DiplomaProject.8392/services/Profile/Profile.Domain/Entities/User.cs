﻿using Profile.Domain.Core;
using Profile.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Entities
{
    public class User: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string UserInfo { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public string MimeType { get; set; }
    }
}