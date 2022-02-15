using Account.API;
using APIGateway.Authentication;
using APIGateway.Authorization.Handlers.Feedback;
using APIGateway.Authorization.Handlers.Post;
using APIGateway.Authorization.Handlers.User;
using APIGateway.Authorization.Requirements.Feedback;
using APIGateway.Authorization.Requirements.Post;
using APIGateway.Authorization.Requirements.User;
using APIGateway.Serialization;
using Booking.API;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PostFeedback.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
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
            //enabling Newtonsoft Json
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    //set custom resolver that allows to ignore properties with JsonIgnore attribute defined in interfaces
                    options.SerializerSettings.ContractResolver = new InterfaceContractResolver();
                    //ignore serialization of properties with null values
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });
            //enabling Swagger
            services.AddSwaggerGen(c =>
            {
                //enabling authorization in swagger with JWT token
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIGateway", Version = "v1" });
                OpenApiSecurityScheme securityDefinition = new()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };
                c.AddSecurityDefinition("jwt_auth", securityDefinition);

                // Make sure swagger UI requires a Bearer token specified
                OpenApiSecurityScheme securityScheme = new()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new()
                {
                    {securityScheme, Array.Empty<string>() },
                };
                c.AddSecurityRequirement(securityRequirements);
            });
            //Add Newtonsoft Json support for Swagger
            services.AddSwaggerGenNewtonsoftSupport();
            //enable authentication with JWT token
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(JWTKeyEncoder.EncodeKey
                    (Configuration["JWTSecretKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddScoped<IAuthenticationManager, AuthenticationManager>(sp =>
           new AuthenticationManager(sp.GetRequiredService<LoginService.LoginServiceClient>(),
           JWTKeyEncoder.EncodeKey(Configuration["JWTSecretKey"])));
            //enable resource based authorization
            services.AddAuthorization(options =>
            {
                //policies
                options.AddPolicy("UserUpdatePolicy", policy =>
                    policy.Requirements.Add(new UserUpdateRequirement()));
                options.AddPolicy("PostUpdatePolicy", policy =>
                    policy.Requirements.Add(new PostUpdateRequirement()));
                options.AddPolicy("PostDeletePolicy", policy =>
                    policy.Requirements.Add(new PostDeleteRequirement()));
                options.AddPolicy("UserFeedbackDeletePolicy", policy =>
                   policy.Requirements.Add(new FeedbackDeleteRequirement<FeedbackForUser.FeedbackForUserClient>()));
                options.AddPolicy("PostFeedbackDeletePolicy", policy =>
                  policy.Requirements.Add(new FeedbackDeleteRequirement<FeedbackForPost.FeedbackForPostClient>()));
            });
            //requirement handlers for resource based authorization
            services.AddSingleton<IAuthorizationHandler, UserUpdateAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, PostUpdateAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, PostDeleteAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, FeedbackDeleteAuthorizationHandler<FeedbackForUser.FeedbackForUserClient>>();
            services.AddSingleton<IAuthorizationHandler, FeedbackDeleteAuthorizationHandler<FeedbackForPost.FeedbackForPostClient>>();
            //registering grpc services
            // account
            var accountUrl = new Uri(Configuration["grpcConnections:account"]);
            //user CRUD servicee
            services.AddGrpcClient<UserService.UserServiceClient>((services, options) =>
            {
                options.Address = accountUrl;
            });
            //user login service
            services.AddGrpcClient<LoginService.LoginServiceClient>((services, options) =>
            {
                options.Address = accountUrl;
            });
            //user related info service
            services.AddGrpcClient<UserRelatedInfoService.UserRelatedInfoServiceClient>((services, options) =>
            {
                options.Address = accountUrl;
            });
            //post
            var postUrl = new Uri(Configuration["grpcConnections:post"]);
            services.AddGrpcClient<PostService.PostServiceClient>((services, options) =>
            {
                options.Address = postUrl;
            });
            services.AddGrpcClient<PostRelatedInfoService.PostRelatedInfoServiceClient>((services, options) =>
            {
                options.Address = postUrl;
            });
            services.AddGrpcClient<FeedbackForUser.FeedbackForUserClient>((services, options) =>
            {
                options.Address = postUrl;
            });
            services.AddGrpcClient<FeedbackForPost.FeedbackForPostClient>((services, options) =>
            {
                options.Address = postUrl;
            });
            //booking 
            var bookingUrl = new Uri(Configuration["grpcConnections:booking"]);
            services.AddGrpcClient<BookingService.BookingServiceClient>((services, options) =>
            {
                options.Address = bookingUrl;
            });

            //enable cors for Blazor app
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44305")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
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
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
