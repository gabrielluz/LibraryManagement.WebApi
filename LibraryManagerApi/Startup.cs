using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagerApi.Filters;
using LibraryManagerApi.Middleware;
using LibraryManagerApi.Models.Dto;
using LibraryManagerApi.Models.Entities;
using LibraryManagerApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CSG_Library_Management
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddeware>();
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    public static class ConfigurationMethods
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDatabaseProvider, MysqlDatabaseProvider>(db => 
                new MysqlDatabaseProvider(connectionString));
            return services;
        }

        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICrudRepository, CrudRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            return services;
        }

        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc(opt => opt.Filters
                .Add(typeof(ValidationActionFilter)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            return services;
        }

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReviewInputDto, Review>()
                .ForPath(e => e.User, opt => opt.Ignore())
                .ForPath(e => e.Book, opt => opt.Ignore());
            CreateMap<Review, ReviewOutputDto>();
            CreateMap<Book, BookInputDto>();
            CreateMap<Book, BookOutputDto>();
            CreateMap<Rental, RentalInputDto>();
            CreateMap<Rental, RentalOutputDto>()
                .ForPath(dto => dto.UserId, opt => opt.MapFrom(entity => entity.User.Id));
        }
    }
}
