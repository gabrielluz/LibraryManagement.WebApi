using LibraryManager.Api.Configuration;
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
        public Token GenerateToken(User user)
        {
            if (user == null)
            {
                return new Token
                {
                    Message = "Invalid user.",
                    Authenticated = false
                };
            }

            var generatedAt = DateTime.UtcNow;
            var expiresAt = generatedAt.AddHours(2);
            var emailIdentity = new GenericIdentity(user.Email, "Email");
            var claims = new[] 
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString())
            };
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "Issuer",
                Audience = "Audience",
                SigningCredentials = Jwt.SigningCredentials,
                Subject = emailIdentity,
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
