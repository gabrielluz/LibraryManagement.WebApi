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
            services
                .Configure<ApiBehaviorOptions>(c => c.SuppressModelStateInvalidFilter = true)
                .AddMvc(opt => opt.Filters
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
}