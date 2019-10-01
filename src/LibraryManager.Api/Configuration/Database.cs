using LibraryManager.Api.Repositories.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LibraryManager.Api.Configuration
{
    public static class Database
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            new MysqlDatabaseProvider(configuration).AssertConnectivity();
            services.AddScoped<IDatabaseProvider, MysqlDatabaseProvider>(db => new MysqlDatabaseProvider(configuration));
            return services;
        }
    }
}
