namespace JobPortalAPI.API.Configurations
{
    public static class Configuration
    {
        static public string? GetTokenAudience
        {
            get
            {

                ConfigurationManager configuration = new();
                configuration.SetBasePath(Directory.GetCurrentDirectory());
                configuration.AddJsonFile("appsettings.json");
                return configuration.GetSection("Token")["Audience"];
            }
        }

        static public string? GetTokenIssuer
        {
            get
            {
                ConfigurationManager configuration = new();
                configuration.SetBasePath(Directory.GetCurrentDirectory());
                configuration.AddJsonFile("appsettings.json");

                return configuration.GetSection("Token")["Issuer"];
            }
        }

        static public string? GetTokenSecurityKey
        {
            get
            {
                ConfigurationManager configuration = new();
                configuration.SetBasePath(Directory.GetCurrentDirectory());
                configuration.AddJsonFile("appsettings.json");

                return configuration.GetSection("Token")["SecurityKey"];
            }
        }
    }
}
