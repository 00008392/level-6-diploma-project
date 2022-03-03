using AutoMapper;
using FrontEndApp.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Mappings
{
    public class AccountMapper: Profile
    {
        public AccountMapper()
        {
            CreateMap<UserResponse, UpdateUser>();
        }
    }
}
