﻿
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
using Account.Domain.Logic.IntegrationEvents.EventHandlers;
using EventBus.SubscriptionManager;
using RabbitMQ.Client;
using Account.API.Mappings;

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
            services.AddGrpc();
            services.AddAutoMapper(typeof(Startup));
            services.AddFluentValidation();
            services.AddDbContext<AccountDbContext>(options =>
options.UseSqlServer(Configuration.GetConnectionString("AccountDbContext")));
            services.AddScoped<DbContext, AccountDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<AbstractValidator<PasswordBaseDTO>, PasswordBaseValidator>();
            services.AddScoped<AbstractValidator<UserRegistrationDTO>, UserRegistrationValidator>();
            services.AddScoped<AbstractValidator<UpdateUserDTO>, UpdateUserValidator>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IPasswordChangeService, PasswordChangeService>();
            services.AddScoped<IEventHandlerService, EventHandlerService>();
            services.AddScoped<IPasswordHandlingService, PasswordHandlingService>();
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
            services.AddTransient<UserUpdatedIntegrationEventHandler>();
            services.AddTransient<UserDeletedIntegrationEventHandler>();

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

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<UserUpdatedIntegrationEvent, UserUpdatedIntegrationEventHandler>();
            eventBus.Subscribe<UserDeletedIntegrationEvent, UserDeletedIntegrationEventHandler>();
            
        }
    }
}
