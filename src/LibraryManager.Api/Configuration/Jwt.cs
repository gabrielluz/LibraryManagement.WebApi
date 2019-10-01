using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Api.Configuration
{
    public static class Jwt
    {
        public static SigningCredentials SigningCredentials;

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
            SigningCredentials = GenerateSigningConfigurations();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt => 
            {
                var paramsValidation = opt.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = SigningCredentials.Key;
                paramsValidation.ValidAudience = "Audience";
                paramsValidation.ValidIssuer = "Issuer";
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

        private static SigningCredentials GenerateSigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                var key = new RsaSecurityKey(provider.ExportParameters(true));
                return new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
            }
        }
    }
}
