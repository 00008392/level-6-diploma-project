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
using FrontEndApp.Services.Authentication.Contracts;

namespace FrontEndApp.Components
{
    //component that all custom components inherit from
    public class CustomBaseComponent: ComponentBase
    {
        [Inject]
        protected NavigationManager _navManager { get; set; }
        [Inject]
        protected AppState _appState { get; set; }
        [Inject]
        protected IAuthenticationService _authService { get; set; }
        [Inject]
        protected LoggedInState _authState { get; set; }
        [Inject]
        protected IJSRuntime _jsRuntime { get; set; }
    }
}
