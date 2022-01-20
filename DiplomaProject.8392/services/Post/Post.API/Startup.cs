using BaseClasses.Contracts;
using BaseClasses.Repositories.EF;
using EventBus.Contracts;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Post.API.Services;
using Post.API.Services.Strategies;
using Post.DAL.EF.Data;
using Post.DAL.EF.Repositories;
using Post.Domain.Core;
using Post.Domain.Entities;
using Post.Domain.Logic.Contracts;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.Services;
using Post.Domain.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Post.Domain.Logic.IntegrationEvents.Events;
using Post.Domain.Logic.IntegrationEvents.EventHandlers;
using EventBus.SubscriptionManager;
using RabbitMQ.Client;

namespace Post.API
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
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<PostDbContext>(options =>
options.UseSqlServer(Configuration.GetConnectionString("PostDbContext")));
            services.AddScoped<DbContext, PostDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRepositoryWithIncludes<Accommodation>, PostRepository>();
            services.AddScoped<IRepositoryWithIncludes<Domain.Entities.User>, UserRepository>();
            services.AddScoped<IRepositoryWithIncludes<Feedback<Domain.Entities.User>>, FeedbackRepository<Domain.Entities.User>>();
            services.AddScoped<IRepositoryWithIncludes<Feedback<Accommodation>>, FeedbackRepository<Accommodation>>();
            services.AddScoped<AbstractValidator<AccommodationManipulationDTO>, PostValidator>();
            services.AddScoped<AbstractValidator<CreateUserDTO>, BaseUserValidator>();
            services.AddScoped<AbstractValidator<UpdateUserDTO>, UpdateUserValidator>();
            services.AddScoped<AbstractValidator<AddBookingDTO>, DatesBookedValidator>();
            services.AddScoped<AbstractValidator<FeedbackDTO>, FeedbackValidator>();
            services.AddScoped<IPostCRUDService, PostCRUDService>();
            services.AddScoped<IInfoService, InfoService>();
            services.AddScoped(typeof(IFeedbackService<,>), typeof(FeedbackService<,>));
            services.AddScoped<IFeedbackValidationService<Domain.Entities.User>, UserFeedbackValidationService>();
            services.AddScoped<IFeedbackValidationService<Accommodation>, AccommodationFeedbackValidationService>();
            services.AddScoped<IEventHandlerService, EventHandlerService>();
            services.AddScoped(typeof(IAccommodationItemsStrategy<,>), typeof(AccommodationItemsStrategy<,>));
            services.AddScoped(typeof(IFeedbackStrategy<,>), typeof(FeedbackStrategy<,>));
            services.AddScoped(typeof(IAcommodationItemsService<,>), typeof(AccommodationItemsService<,>));
            services.AddSingleton<ISubscriptionManager, EventBusSubscriptionManager>();
            services.AddSingleton<IEventBus, RabbitMQEventBus.EventBus.RabbitMQEventBus>(sp => {

                var queueName = "post_queue";
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
            services.AddTransient<UserDeletedIntegrationEventHandler>();
            services.AddTransient<UserUpdatedIntegrationEventHandler>();
            services.AddTransient<UserCreatedIntegrationEventHandler>();
            services.AddTransient<AccommodationBookedIntegrationEventHandler>();
            services.AddTransient<AccommodationBookingCancelledIntegrationEventHandler>();
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
                endpoints.MapGrpcService<PostCRUDServiceGrpc>();
                endpoints.MapGrpcService<PostInfoServiceGrpc>();
                endpoints.MapGrpcService<AccommodationRulesServiceGrpc>();
                endpoints.MapGrpcService<AccommodationFacilitiesServiceGrpc>();
                endpoints.MapGrpcService<AccommodationSpecificitiesServiceGrpc>();
                endpoints.MapGrpcService<FeedbackForUserServiceGrpc>();
                endpoints.MapGrpcService<FeedbackForAccommodationServiceGrpc>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });

            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();
            eventBus.Subscribe<UserDeletedIntegrationEvent, UserDeletedIntegrationEventHandler>();
            eventBus.Subscribe<UserUpdatedIntegrationEvent, UserUpdatedIntegrationEventHandler>();
            eventBus.Subscribe<AccommodationBookedIntegrationEvent, 
                AccommodationBookedIntegrationEventHandler>();
            eventBus.Subscribe<AccommodationBookingCancelledIntegrationEvent,
                AccommodationBookingCancelledIntegrationEventHandler>();
        }
    }
}
