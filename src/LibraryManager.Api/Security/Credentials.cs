using System.Security.Cryptography;
using System.Text;

namespace LibraryManager.Api.Security
{
    public class Credentials
    {
        public string Email { get; }
        public string Password { get; }

        public Credentials(string email, string password)
        {
            Email = email;
            Password = Hash(password);
        }

        private string Hash(string plainText)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(plainText);
                byte[] hash = sha512.ComputeHash(bytes);

                StringBuilder result = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                    result.Append(hash[i].ToString("X2"));

                return result.ToString();
            }
        }
    }
}
