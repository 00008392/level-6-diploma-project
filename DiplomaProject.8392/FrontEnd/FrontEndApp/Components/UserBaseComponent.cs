using FrontEndApp.Services.User.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndApp.Components
{
    //component that all user components inherit from
    public class UserBaseComponent: CustomBaseComponent
    {
        [Inject]
        protected IUserService _service { get; set; }
    }
}
