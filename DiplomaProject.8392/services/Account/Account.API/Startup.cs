using Account.DAL.EF.Data;
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
using BaseClasses.Contracts;
using BaseClasses.Repositories.EF;
using EventBus.Contracts;
using Account.Domain.Logic.IntegrationEvents.Events;
using EventBus.SubscriptionManager;
using RabbitMQ.Client;
using Account.API.Mappings;
using Account.DAL.EF.Repository;
using Account.Domain.Entities;

namespace Account.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //enabling grpc
            services.AddGrpc();
            //configuring automapper
            services.AddAutoMapper(typeof(Startup));
            //enabling fluent validation
            services.AddFluentValidation();
            //configuring db context
            services.AddDbContext<AccountDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AccountDbContext")));
            services.AddScoped<DbContext, AccountDbContext>();
            //registering repositories
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IRepositoryWithIncludes<User>), typeof(AccountRepository));
            //registering validators
            services.AddScoped<AbstractValidator<IPasswordBaseDTO>, PasswordValidator>();
            services.AddScoped<AbstractValidator<UserRegistrationDTO>, UserRegistrationValidator>();
            services.AddScoped<AbstractValidator<UserBaseDTO>, UserValidator>();
            //registering business logic services
            services.AddScoped<ILoginService, Domain.Logic.Services.LoginService>();
            services.AddScoped<IUserService, Domain.Logic.Services.UserService>();
            services.AddScoped<IUserRelatedInfoService, Domain.Logic.Services.UserRelatedInfoService>();
            //registering helpers
            services.AddScoped<IPasswordHandlingService, PasswordHandlingService>();
            //configuring and registering event bus
            services.AddSingleton<ISubscriptionManager, EventBusSubscriptionManager>();
            services.AddSingleton<IEventBus, RabbitMQEventBus.EventBus.RabbitMQEventBus>(sp => {
          
                var queueName = "account_queue";
                var subsManager = sp.GetRequiredService<ISubscriptionManager>();
                var serviceFactory = sp.GetRequiredService<IServiceScopeFactory>();
                var factory = new ConnectionFactory() {
                    HostName = "localhost",
                    DispatchConsumersAsync = true
                };
                var connection = factory.CreateConnection();
                return new RabbitMQEventBus.EventBus.RabbitMQEventBus(queueName, subsManager,
                    serviceFactory, connection);
            }
            );
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
                //adding grpc services
                GrpcEndpointRouteBuilderExtensions.MapGrpcService<LoginServiceGrpc>(endpoints);
                GrpcEndpointRouteBuilderExtensions.MapGrpcService<UserRelatedInfoServiceGrpc>(endpoints);
                GrpcEndpointRouteBuilderExtensions.MapGrpcService<UserServiceGrpc>(endpoints);
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            }));
            
        }
    }
}
