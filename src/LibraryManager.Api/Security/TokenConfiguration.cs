using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace LibraryManager.Api.Security
{
    public class TokenConfiguration
    {
        public TokenConfiguration()
        {
            GenerateSigningConfigurations();
        }

        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationInSeconds { get; set; }
        public SigningCredentials SigningCredentials { get; private set; }

        private void GenerateSigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                var key = new RsaSecurityKey(provider.ExportParameters(true));
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
            }
        }
    }
}
