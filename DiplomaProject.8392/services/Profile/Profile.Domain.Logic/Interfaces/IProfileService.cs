﻿using Profile.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.Interfaces
{
    interface IProfileService
    {
        Task UpdateProfile(UpdateProfileDTO profile);
        Task DeleteProfile(long id);
        Task ChangePassword(ChangePasswordDTO password);
    }
}