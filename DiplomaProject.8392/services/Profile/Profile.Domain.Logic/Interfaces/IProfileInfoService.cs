using Profile.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.Interfaces
{
    interface IProfileInfoService
    {
        Task<ProfileInfoDTO> GetProfileInfo(long id);
    }
}
