using LibraryManager.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LibraryManager.Api.Configuration
{
    public static class Jwt
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfiguration = configuration
                .GetSection(nameof(TokenConfiguration))
                .Get<TokenConfiguration>();

            services.AddSingleton(tokenConfiguration);
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                var paramsValidation = opt.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = tokenConfiguration.SigningCredentials.Key;
                paramsValidation.ValidAudience = tokenConfiguration.Audience;
                paramsValidation.ValidIssuer = tokenConfiguration.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                   .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                   .RequireAuthenticatedUser().Build());
            });

            return services;
        }
    }
}
