using System;
using System.Security.Cryptography;
using System.Text;

namespace UserManagement.Domain.Entities
{
    public  class UserToken
    {
        public UserToken(string email, string redirectUri) : this(email, redirectUri, DateTime.Now.AddMinutes(10).Ticks)
        {
            
        }

        public UserToken(string email, string redirectUri, long authorizationCodeExpiration)
        {
            Email = email;
            RedirectUri = redirectUri;
            AuthorizationCodeExpiration = authorizationCodeExpiration;
            IsCanceled = false;

            AuthorizationCodeGenerate();
        }

        public string Id { get; private set; }
        public string Email { get; private set; }
        public string AuthorizationCode { get; private set; }
        public string RedirectUri { get; private set; }
        public long AuthorizationCodeExpiration { get; private set; }
        public string Token { get; private set; }
        public long TokenExpiration { get; private set; }
        public string RefreshToken { get; private set; }
        public bool IsCanceled { get; private set; }

        public void SetToken(string token)
        {
            SetToken(token, DateTime.Now.AddHours(1).Ticks, null);
        }

        public void SetToken(string token, long tokenExpiration)
        {
            SetToken(token, tokenExpiration, null);
        }

        public void SetToken(string token, long tokenExpiration, string refreshToken)
        {
            Token = token;
            TokenExpiration = tokenExpiration;
            RefreshToken = refreshToken;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }

        private void AuthorizationCodeGenerate()
        {
            var code = $"{Email}]:[{AuthorizationCodeExpiration}]:[{RandomNumberGenerator.GetInt32(Email.Length)}";
            var codeBytes = Encoding.UTF8.GetBytes(code);
            AuthorizationCode = Convert.ToBase64String(codeBytes);
        }
    }
}
