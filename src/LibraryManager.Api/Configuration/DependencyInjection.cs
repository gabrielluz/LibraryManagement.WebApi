using LibraryManager.Api.Exceptions.Handlers;
using LibraryManager.Api.Repositories.Implementations;
using LibraryManager.Api.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManager.Api.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ICrudRepository, CrudRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddSingleton<ExceptionHandlerAbstractFactory>();
            return services;
        }
    }
}
