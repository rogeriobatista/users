using System;
using Microsoft.Extensions.Configuration;
using Users.Generics.Interfaces;

namespace Users.Generics.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        private IConfiguration Configuration { get; }

        public TokenHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public bool Validate(string token)
        {
            string secret = Configuration.GetSection("AppSettings").GetValue<string>("Secret");

            if (string.IsNullOrEmpty(secret))
                return false;

            if (token == secret)
                return true;

            return false;
        }
    }
}
