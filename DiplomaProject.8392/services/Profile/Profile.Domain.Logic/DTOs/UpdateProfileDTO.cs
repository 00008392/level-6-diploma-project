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
        public long? CityId { get;private set; }
        public UpdateProfileDTO(long id, string firstName,
         string lastName, string email,
         string phoneNumber, DateTime? dateOfBirth,
         Gender? gender, string address,
         string userInfo, long? cityId) : base(id, firstName,
             lastName, email, phoneNumber,
             dateOfBirth, gender, address, userInfo)
        {
            CityId = cityId;
        }
    }
}
