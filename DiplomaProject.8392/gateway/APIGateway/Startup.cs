using Account.API;
using Booking.API;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Post.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIGateway", Version = "v1" });
            });
            // account
            var accountUrl = new Uri(Configuration["grpcConnections:account"]);
            services.AddGrpcClient<UserManipulation.UserManipulationClient>((services, options) =>
            {
                options.Address = accountUrl;
            });
            services.AddGrpcClient<Login.LoginClient>((services, options) =>
            {
                options.Address = accountUrl;
            });
            services.AddGrpcClient<UserInfo.UserInfoClient>((services, options) =>
            {
                options.Address = accountUrl;
            });
            //post
            var postUrl = new Uri(Configuration["grpcConnections:post"]);
            services.AddGrpcClient<PostCRUD.PostCRUDClient>((services, options) =>
            {
                options.Address = postUrl;
            });
            services.AddGrpcClient<AccommodationRules.AccommodationRulesClient>((services, options) =>
            {
                options.Address = postUrl;
            });
            services.AddGrpcClient<AccommodationFacilities.AccommodationFacilitiesClient>((services, options) =>
            {
                options.Address = postUrl;
            });
            services.AddGrpcClient<AccommodationSpecificities.AccommodationSpecificitiesClient>((services, options) =>
            {
                options.Address = postUrl;
            });
            services.AddGrpcClient<FeedbackForUser.FeedbackForUserClient>((services, options) =>
            {
                options.Address = postUrl;
            });
            services.AddGrpcClient<FeedbackForAccommodation.FeedbackForAccommodationClient>((services, options) =>
            {
                options.Address = postUrl;
            });
            //booking 
            var bookingUrl = new Uri(Configuration["grpcConnections:booking"]);
            services.AddGrpcClient<BookingService.BookingServiceClient>((services, options) =>
            {
                options.Address = bookingUrl;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIGateway v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
