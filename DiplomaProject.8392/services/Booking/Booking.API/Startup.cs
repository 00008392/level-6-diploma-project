using BaseClasses.Contracts;
using BaseClasses.Repositories.EF;
using Booking.DAL.EF.Data;
using Booking.Domain.Logic.Contracts;
using Booking.Domain.Logic.DTOs;
using Booking.Domain.Logic.Services;
using Booking.Domain.Logic.Validation;
using Booking.Domain.Logic.IntegrationEvents.EventHandlers;
using Booking.Domain.Logic.IntegrationEvents.Events;
using EventBus.Contracts;
using EventBus.SubscriptionManager;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQEventBus.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking.API.Services;

namespace Booking.API
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
            services.AddFluentValidation();
            services.AddDbContext<BookingDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("BookingDbContext")));
            services.AddScoped<DbContext, BookingDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<AbstractValidator<BaseAccommodationDTO>, BaseAccommodationValidator>();
            services.AddScoped<AbstractValidator<CreateBookingRequestDTO>, BookingRequestValidator>();
            services.AddScoped<AbstractValidator<CreateUserDTO>, CreateUserValidator>();
            services.AddScoped<AbstractValidator<UserDTO>, UpdateUserValidator>();
            services.AddScoped<IBookingInfoService, BookingInfoService>();
            services.AddScoped<IBookingRequestManipulationService, BookingRequestManipulationService>();
            services.AddScoped<IEventHandlerService<Domain.Entities.User>, UserEventHandlerService>();
            services.AddScoped<IEventHandlerService<Domain.Entities.Accommodation>, AccommodationEventHandlerService>();
            services.AddSingleton<ISubscriptionManager, EventBusSubscriptionManager>();
            services.AddSingleton<IEventBus, RabbitMQEventBus.EventBus.RabbitMQEventBus>(sp => {

                var queueName = "booking_queue";
                var subsManager = sp.GetRequiredService<ISubscriptionManager>();
                var serviceFactory = sp.GetRequiredService<IServiceScopeFactory>();
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    DispatchConsumersAsync = true
                };
                var connection = factory.CreateConnection();
                return new RabbitMQEventBus.EventBus.RabbitMQEventBus(queueName, subsManager,
                    serviceFactory, connection);
            }
            );
            services.AddTransient<UserCreatedIntegrationEventHandler>();
            services.AddTransient<UserDeletedIntegrationEventHandler>();
            services.AddTransient<UserUpdatedIntegrationEventHandler>();
            services.AddTransient<AccommodationCreatedIntegrationEventHandler>();
            services.AddTransient<AccommodationUpdatedIntegrationEventHandler>();
            services.AddTransient<AccommodationDeletedIntegrationEventhandler>();
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
                endpoints.MapGrpcService<BookingInfoServiceGrpc>();
                endpoints.MapGrpcService<BookingManipulationServiceGrpc>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();
            eventBus.Subscribe<UserUpdatedIntegrationEvent, UserUpdatedIntegrationEventHandler>();
            eventBus.Subscribe<UserDeletedIntegrationEvent, UserDeletedIntegrationEventHandler>();
            eventBus.Subscribe<AccommodationCreatedIntegrationEvent, AccommodationCreatedIntegrationEventHandler>();
            eventBus.Subscribe<AccommodationUpdatedIntegrationEvent, AccommodationUpdatedIntegrationEventHandler>();
            eventBus.Subscribe<AccommodationDeletedIntegrationEvent, AccommodationDeletedIntegrationEventhandler>();
        }
    }
}
