using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API
{
    //IAccountRequest - in order to send data in TimeStamp format to the service instead of DateTime 
    //and in order to hide TimeStamp format from user
    public partial class RegisterRequest: IAccountRequest
    {
        public DateTime? DateOfBirth { get; set; }
    }
}
