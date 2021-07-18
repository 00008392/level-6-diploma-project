using Profile.Domain.Entities;
using Profile.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.DTOs
{
   public class UpdateProfileDTO: BaseProfileDTO
    {
        public long? CityId { get; set; }
    }
}
