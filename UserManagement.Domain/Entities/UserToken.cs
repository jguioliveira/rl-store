using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Domain.Entities
{
    public  class UserToken
    {
        public UserToken(string email, string authorizationCode, string redirectUri, string authorizationCodeExpiration)
        {
            Email = email;
            AuthorizationCode = authorizationCode;
            RedirectUri = redirectUri;
            AuthorizationCodeExpiration = authorizationCodeExpiration;
            IsCanceled = false;
        }

        public string Id { get; private set; }
        public string Email { get; private set; }
        public string AuthorizationCode { get; private set; }
        public string RedirectUri { get; private set; }
        public string AuthorizationCodeExpiration { get; private set; }
        public string Token { get; private set; }
        public string TokenExpiration { get; private set; }
        public string RefreshToken { get; private set; }
        public bool IsCanceled { get; private set; }
    }
}
