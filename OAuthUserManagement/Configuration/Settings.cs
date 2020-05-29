namespace UserManagement.OAuth.Configuration
{
    public class Settings
    {
        public OAuthSettings OAuthSettings { get; set; }
    }

    public class OAuthSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
    }
}
