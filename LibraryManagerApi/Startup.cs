﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagerApi.Controllers;
using LibraryManagerApi.Filters;
using LibraryManagerApi.Middleware;
using LibraryManagerApi.Models.Dto;
using LibraryManagerApi.Models.Entities;
using LibraryManagerApi.Repositories;
using LibraryManagerApi.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LibraryManagerApi
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
            var connectionString = Configuration.GetConnectionString("LibraryManagerApiConnection");
            services.ConfigureDatabase(connectionString);
            services.ConfigureDependencyInjection();
            services.ConfigureMvc();
            services.AddAutoMapper();
            services.AddSwaggerDocument(sc => 
            {
                sc.Title = "Library Management API";
                sc.DocumentName = "Library Management API Documentation";
                sc.Version = "1";
                sc.Description = "This is a small API I use to learn new things related to ASP.NET Core, "
                    + "web APIs, and some .NET frameworks in general such as Dapper, NSwag, etc.";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddeware>();
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi3();
        }
    }
}
