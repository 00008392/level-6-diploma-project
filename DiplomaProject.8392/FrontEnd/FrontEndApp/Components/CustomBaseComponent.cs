using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndApp.Services;
using Blazored.SessionStorage;
using System.Net.Http;
using FrontEndApp.States;
using Microsoft.JSInterop;
using FrontEndApp.Services.Authentication;

namespace FrontEndApp.Components
{
    public class CustomBaseComponent: ComponentBase
    {
        [Inject]
        protected NavigationManager _navManager { get; set; }
        [Inject]
        protected IAuthenticationService _authService { get; set; }
        [Inject]
        protected LoggedInState _authState { get; set; }

    }
}
