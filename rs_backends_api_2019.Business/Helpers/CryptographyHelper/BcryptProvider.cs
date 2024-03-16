using Microsoft.Extensions.Configuration;
using rs_backends_api_2019.Business.Helpers.CryptographyHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.CryptographyHelper
{
    public class BcryptProvider : IBcryptProvider
    {
        private readonly IConfiguration _configuration;

        private string _saltingPassword { get; set; } = string.Empty;

        public BcryptProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _saltingPassword = _configuration["Crypt:SaltingPassword"];
        }

        public bool Check(string input, string hashing)
        {
            return BCrypt.Net.BCrypt.Verify(input, hashing);
        }

        public string Hash(string input)
        {
            return BCrypt.Net.BCrypt.HashPassword(input, _saltingPassword);
        }
    }
}
