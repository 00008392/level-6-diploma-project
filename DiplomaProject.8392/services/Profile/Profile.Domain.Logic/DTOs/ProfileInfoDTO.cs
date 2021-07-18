using Profile.Domain.Entities;
using Profile.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Domain.Logic.DTOs
{
    public class ProfileInfoDTO: BaseProfileDTO
    {
        public DateTime RegistrationDate { get; set; }
        public City City { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public string MimeType { get; set; }
    }
}
