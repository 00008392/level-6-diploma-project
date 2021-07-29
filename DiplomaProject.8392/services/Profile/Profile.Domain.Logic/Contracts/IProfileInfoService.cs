using Profile.Domain.Logic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.Contracts
{
    public interface IProfileInfoService
    {
        Task<ProfileInfoDTO> GetProfileInfoAsync(long id);
    }
}
