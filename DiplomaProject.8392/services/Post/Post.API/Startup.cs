﻿using FluentValidation;
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
using Post.Domain.Logic.Core;
using Post.Domain.Logic.DTOs;
using Post.Domain.Logic.Services;
using Post.Domain.Logic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            services.AddScoped<AbstractValidator<AccommodaitonManipulationDTO>, PostValidator>();
            services.AddScoped<AbstractValidator<CreateUserDTO>, BaseUserValidator>();
            services.AddScoped<AbstractValidator<UpdateUserDTO>, UpdateUserValidator>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPostRepository,PostRepository>();
            services.AddScoped<IPostCRUDService, PostCRUDService>();
            services.AddScoped<IEventHandlerService, EventHandlerService>();
            services.AddScoped(typeof(IPostRelatedInfoService<,>), typeof(PostRelatedInfoService<,>));
            services.AddScoped(typeof(IPostItemsService<>), typeof(PostItemsService<>));
            services.AddScoped(typeof(IPostRelatedInfoStrategy<,>), typeof(PostRelatedInfoGenericStrategy<,>));
            services.AddScoped(typeof(IPostItemsStrategy<>), typeof(PostItemsGenericStrategy<>));
            services.AddDbContext<PostDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("PostDbContext")));
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
                endpoints.MapGrpcService<PostRelatedInfoServiceGrpc>();
                endpoints.MapGrpcService<PostItemsServiceGrpc>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
