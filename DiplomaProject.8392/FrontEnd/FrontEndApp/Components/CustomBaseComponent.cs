using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndApp.Services;
using Blazored.SessionStorage;
using System.Net.Http;
using FrontEndApp.States;

namespace FrontEndApp.Components
{
    public class CustomBaseComponent: ComponentBase
    {
        //protected bool _isAuthenticated = false;
        [Inject]
        protected HttpClient client { get; set; }
        [Inject]
        protected NavigationManager navManager { get; set; }
        [Inject]
        protected IAuthenticationService authService { get; set; }
        [Inject]
        protected ISessionStorageService storage { get; set; }
        [Inject]
        protected LoggedInState AuthState { get; set; }
    }
}
