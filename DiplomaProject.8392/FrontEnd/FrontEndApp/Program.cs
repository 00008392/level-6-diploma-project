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
using FrontEndApp.Services.Booking.Contracts;
using FrontEndApp.Services.Booking;
using FrontEndApp.Services.Feedback.Contracts;
using FrontEndApp.Models.User;
using FrontEndApp.Services.Feedback;
using FrontEndApp.Models.Post;
using FrontEndApp.Services.Authentication.Contracts;

namespace FrontEndApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            //register blazorise
            builder.Services
            .AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true;
            })
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();
            //register http client
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5002/") });
            //register session storage
            builder.Services.AddBlazoredSessionStorage();
            //register states
            builder.Services.AddScoped<LoggedInState>();
            builder.Services.AddScoped<AppState>();
            //register services
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRelatedInfoService, UserRelatedInfoService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IPostRelatedInfoService, PostRelatedInfoService>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IFeedbackService<UserResponse>, FeedbackServiceForUser>();
            builder.Services.AddScoped<IFeedbackService<PostResponse>, FeedbackServiceForPost>();
            //register automapper
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            await builder.Build().RunAsync();
        }
    }
}
