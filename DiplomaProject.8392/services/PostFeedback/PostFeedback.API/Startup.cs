
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
using PostFeedback.DAL.EF.Repositories;
using DAL.Base.Contracts;
using DAL.Base.Repositories;

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
            //enabling grpc
            services.AddGrpc();
            //enabling fluent validation
            services.AddFluentValidation();
            //configuring automapper
            services.AddAutoMapper(typeof(Startup));
            //configuring and registering db context
            services.AddDbContext<PostDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PostDbContext")));
            services.AddScoped<DbContext, PostDbContext>();
            //registering repositories
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRepository<Post>, PostRepository>();
            //registering validators
            services.AddScoped<AbstractValidator<PostManipulationDTO>, PostValidator>();
            services.AddScoped<AbstractValidator<FeedbackDTO>, FeedbackValidator>();
            services.AddScoped<AbstractValidator<PhotoDTO>, PhotoValidator>();
            //registering business logic services
            services.AddScoped<IPostService, Domain.Logic.Services.PostService>();
            services.AddScoped(typeof(IPostRelatedInfoService<>), typeof(PostRelatedInfoService<>));
            services.AddScoped(typeof(IFeedbackService<,>), typeof(FeedbackService<,>));
            services.AddScoped(typeof(IPostPhotoService), typeof(Domain.Logic.Services.PostPhotoService));
            services.AddScoped<IFeedbackValidationService<Domain.Entities.User>, UserFeedbackValidationService>();
            services.AddScoped<IFeedbackValidationService<Post>, PostFeedbackValidationService>();
            //registering grpc strategies
            services.AddScoped(typeof(IFeedbackStrategy<,>), typeof(FeedbackStrategy<,>));
            //configuring and registering event bus
            services.AddSingleton<ISubscriptionManager, EventBusSubscriptionManager>();
            services.AddSingleton<IEventBus, RabbitMQEventBus.EventBus.RabbitMQEventBus>(sp => {

                var queueName = "post_queue";
                var subsManager = sp.GetRequiredService<ISubscriptionManager>();
                var serviceFactory = sp.GetRequiredService<IServiceScopeFactory>();
                var factory = new ConnectionFactory() {
                    Uri = new Uri(Configuration["RabbitMQ:uri"]),
                    DispatchConsumersAsync = true,
                    UserName = Configuration["RabbitMQ:username"],
                    Password = Configuration["RabbitMQ:password"]
                };
                var connection = factory.CreateConnection();
                return new RabbitMQEventBus.EventBus.RabbitMQEventBus(queueName, subsManager,
                    serviceFactory, connection);
            }
          );
            //registering event handlers
            services.AddTransient<UserDeletedIntegrationEventHandler>();
            services.AddTransient<UserCreatedOrUpdatedIntegrationEventHandler>();
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
                //adding grpc services
                endpoints.MapGrpcService<PostServiceGrpc>();
                endpoints.MapGrpcService<PostPhotoServiceGrpc>();
                endpoints.MapGrpcService<PostRelatedInfoServiceGrpc>();
                endpoints.MapGrpcService<FeedbackForUserServiceGrpc>();
                endpoints.MapGrpcService<FeedbackForPostServiceGrpc>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
            //subscribing to events from other microservices
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<UserDeletedIntegrationEvent, UserDeletedIntegrationEventHandler>();
            eventBus.Subscribe<UserCreatedOrUpdatedIntegrationEvent, UserCreatedOrUpdatedIntegrationEventHandler>();
            eventBus.Subscribe<BookingAcceptedIntegrationEvent, 
                BookingAcceptedIntegrationEventHandler>();
            eventBus.Subscribe<BookingCancelledIntegrationEvent,
                BookingCancelledIntegrationEventHandler>();
        }
    }
}
