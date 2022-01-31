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
using PostFeedback.API.Services;
using PostFeedback.API.Services.Strategies;
using PostFeedback.DAL.EF.Data;
using PostFeedback.DAL.EF.Repositories;
using PostFeedback.Domain.Entities;
using PostFeedback.Domain.Logic.Contracts;
using PostFeedback.Domain.Logic.DTOs;
using PostFeedback.Domain.Logic.Services;
using PostFeedback.Domain.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostFeedback.Domain.Logic.IntegrationEvents.Events;
using PostFeedback.Domain.Logic.IntegrationEvents.EventHandlers;
using EventBus.SubscriptionManager;
using RabbitMQ.Client;

namespace PostFeedback.API
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
            services.AddScoped<IRepositoryWithIncludes<Post>, PostRepository>();
            services.AddScoped<IRepositoryWithIncludes<Domain.Entities.User>, UserRepository>();
            services.AddScoped<IRepositoryWithIncludes<Feedback<Domain.Entities.User>>, FeedbackRepository<Domain.Entities.User>>();
            services.AddScoped<IRepositoryWithIncludes<Feedback<Post>>, FeedbackRepository<Post>>();
            services.AddScoped<AbstractValidator<PostManipulationDTO>, PostValidator>();
            services.AddScoped<AbstractValidator<UserDTO>, UserValidator>();
            services.AddScoped<AbstractValidator<AddBookingDTO>, BookingValidator>();
            services.AddScoped<AbstractValidator<FeedbackDTO>, FeedbackValidator>();
            services.AddScoped<IPostCRUDService, PostCRUDService>();
            services.AddScoped<IInfoService, InfoService>();
            services.AddScoped(typeof(IFeedbackService<,>), typeof(FeedbackService<,>));
            services.AddScoped<IFeedbackValidationService<Domain.Entities.User>, UserFeedbackValidationService>();
            services.AddScoped<IFeedbackValidationService<Post>, PostFeedbackValidationService>();
            services.AddScoped<IEventHandlerService, EventHandlerService>();
            services.AddScoped(typeof(IPostItemsStrategy<,>), typeof(PostItemsStrategy<,>));
            services.AddScoped(typeof(IFeedbackStrategy<,>), typeof(FeedbackStrategy<,>));
            services.AddScoped(typeof(IPostItemsService<,>), typeof(PostItemsService<,>));
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
            services.AddTransient<BookingAcceptedIntegrationEventHandler>();
            services.AddTransient<BookingCancelledIntegrationEventHandler>();
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
                endpoints.MapGrpcService<RulesServiceGrpc>();
                endpoints.MapGrpcService<FacilitiesServiceGrpc>();
                endpoints.MapGrpcService<SpecificitiesServiceGrpc>();
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
            eventBus.Subscribe<BookingAcceptedIntegrationEvent, 
                BookingAcceptedIntegrationEventHandler>();
            eventBus.Subscribe<BookingCancelledIntegrationEvent,
                BookingCancelledIntegrationEventHandler>();
        }
    }
}
