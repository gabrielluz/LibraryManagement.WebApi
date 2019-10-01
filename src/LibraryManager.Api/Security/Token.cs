using System;

namespace LibraryManager.Api.Security
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime GeneratedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool Authenticated { get; set; }
        public string Message { get; set; }
    }
}