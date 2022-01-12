using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Models
{
    public class EditUser: UserBase
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public long CountryId { get; set; }
        public string UserInfo { get; set; }
        //to populate dropdown
        public ICollection<Country> CountryList { get; set; }
        //to be displayed in editing page
        public Country Country { get; set; }
    }
}
