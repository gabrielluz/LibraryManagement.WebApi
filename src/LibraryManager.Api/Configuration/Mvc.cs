using LibraryManager.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManager.Api.Configuration
{
    public static class Mvc
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.AddApiVersioning();

            services.AddVersionedApiExplorer(p =>
            {
                p.SubstituteApiVersionInUrl = true;
            });

            services.Configure<ApiBehaviorOptions>(config => config.SuppressModelStateInvalidFilter = true);
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(ExceptionFilter));
                opt.Filters.Add(typeof(ValidationActionFilter));
            })
            .AddXmlSerializerFormatters()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;
        }
    }
}
