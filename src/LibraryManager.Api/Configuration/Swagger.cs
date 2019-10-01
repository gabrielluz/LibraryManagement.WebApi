using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManager.Api.Configuration
{
    public static class Swagger
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerDocument(sc =>
            {
                sc.Title = "Library Management API";
                sc.DocumentName = "v1";
                sc.Description = "This is a small API I use to learn new things related to ASP.NET Core, "
                    + "web APIs, and some .NET frameworks in general such as Dapper, NSwag, etc.";
            });
        }
    }
}
