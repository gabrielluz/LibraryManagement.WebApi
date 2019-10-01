using LibraryManager.Api.Models.Entities;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManager.Api.Security
{
    public interface ISecurityManager
    {
        Token GenerateToken(User user);
    }
}
