using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Profile.API.Services;
using Profile.DAL.EF.Data;
using Profile.DAL.EF.Repositories;
using Profile.Domain.Core;
using Profile.Domain.Logic.DTOs;
using Profile.Domain.Logic.Contracts;
using Profile.Domain.Logic.Services;
using Profile.Domain.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profile.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddFluentValidation();
            services.AddScoped<AbstractValidator<UpdateProfileDTO>, ProfileValidator>();
            services.AddScoped(typeof(IProfileInfoService), typeof(ProfileInfoService));
            services.AddScoped(typeof(IProfileService), typeof(ProfileService));
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));       
            services.AddDbContext<ProfileDbContext>(options =>
         options.UseSqlServer(Configuration.GetConnectionString("ProfileDbContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ProfileServiceGrpc>();
                endpoints.MapGrpcService<ProfileInfoServiceGrpc>();
        

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
