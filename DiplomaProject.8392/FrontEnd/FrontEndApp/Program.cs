using Blazored.LocalStorage;
using Blazored.SessionStorage;
using FrontEndApp.Services;
using FrontEndApp.Services.User;
using FrontEndApp.Services.User.Contracts;
using FrontEndApp.Services.Authentication;
using FrontEndApp.States;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FrontEndApp.Services.Post.Contracts;
using FrontEndApp.Services.Post;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

namespace FrontEndApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services
            .AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true;
            })
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44377/") });
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddBlazoredSessionStorage();
            builder.Services.AddScoped<LoggedInState>();
            builder.Services.AddScoped<AppState>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRelatedInfoService, UserRelatedInfoService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IPostRelatedInfoService, PostRelatedInfoService>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            await builder.Build().RunAsync();
        }
    }
}
