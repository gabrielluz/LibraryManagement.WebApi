using AutoMapper;
using LibraryManagerApi.Filters;
using LibraryManagerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagerApi.Utils
{
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
            services.Configure<ApiBehaviorOptions>(config => config.SuppressModelStateInvalidFilter = true);
            
            services.AddMvc(opt => opt.Filters.Add(typeof(ValidationActionFilter)))
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

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerDocument(sc => 
            {
                sc.Title = "Library Management API";
                sc.DocumentName = "Library Management API Documentation";
                sc.Version = "1";
                sc.Description = "This is a small API I use to learn new things related to ASP.NET Core, "
                    + "web APIs, and some .NET frameworks in general such as Dapper, NSwag, etc.";
            });
        }
    }
}