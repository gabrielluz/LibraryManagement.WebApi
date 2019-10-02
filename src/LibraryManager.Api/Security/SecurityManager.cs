using LibraryManager.Api.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace LibraryManager.Api.Security
{
    public class SecurityManager : ISecurityManager
    {
        private readonly TokenConfiguration _tokenConfiguration;

        public SecurityManager(TokenConfiguration tokenConfiguration)
        {
            _tokenConfiguration = tokenConfiguration;
        }

        public Token GenerateToken(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "Email"),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                });
            var handler = new JwtSecurityTokenHandler();
            var generatedAt = DateTime.UtcNow;
            var expiresAt = generatedAt.AddSeconds(_tokenConfiguration.DurationInSeconds);
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _tokenConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = generatedAt,
                Expires = expiresAt
            });

            return new Token
            {
                Authenticated = true,
                GeneratedAt = generatedAt,
                ExpiresAt = expiresAt,
                Message = "OK",
                AccessToken = handler.WriteToken(securityToken)
            };
        }
    }
}
