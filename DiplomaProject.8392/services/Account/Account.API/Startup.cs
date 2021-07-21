
using Account.DAL.EF.Data;
using Account.DAL.EF.Repositories;
using Account.Domain.Core;
using Account.Domain.Logic.DTOs;
using Account.Domain.Logic.Contracts;
using Account.Domain.Logic.Services;
using Account.Domain.Logic.Validation;
using Account.PasswordHandling;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.API.Services;

namespace Account.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddFluentValidation();
            services.AddScoped<AbstractValidator<PasswordBaseDTO>, PasswordBaseValidator>();
            services.AddScoped<AbstractValidator<UserRegistrationDTO>, UserRegistrationValidator>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>) );
            services.AddScoped(typeof(ILoginService), typeof(LoginService));
            services.AddScoped(typeof(IRegistrationService), typeof(RegistrationService));
            services.AddScoped(typeof(IPasswordChangeService), typeof(PasswordChangeService));
            services.AddScoped(typeof(IPasswordHandlingService), typeof(PasswordHandlingService));
            services.AddDbContext<AccountDbContext>(options =>
         options.UseSqlServer(Configuration.GetConnectionString("AccountDbContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints((Action<Microsoft.AspNetCore.Routing.IEndpointRouteBuilder>)(endpoints =>
            {
                GrpcEndpointRouteBuilderExtensions.MapGrpcService<LoginServiceGrpc>(endpoints);
                GrpcEndpointRouteBuilderExtensions.MapGrpcService<RegistrationServiceGrpc>(endpoints);
                GrpcEndpointRouteBuilderExtensions.MapGrpcService<PasswordChangeServiceGrpc>(endpoints);

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            }));
        }
    }
}
