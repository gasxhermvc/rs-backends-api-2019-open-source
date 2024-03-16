using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using rs_backends_api_2019.Business.Global;
using rs_backends_api_2019.Business.Helpers.CryptographyHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using rs_backends_api_2019.Business.Extensions;

namespace rs_backends_api_2019.Business.Helpers.CryptographyHelper
{
    public class CryptographyService : ICryptographyService
    {
        public ICryptographyProvider CryptHelper(IConfiguration configuration)
        {
            this.CheckCreateKey(configuration);

            ICryptographyProvider provider = null;

            provider = new CryptographyProvider(configuration);


            return provider;
        }

        public IBcryptProvider BcryptHelper(IConfiguration configuration)
        {
            this.CheckCreateKey(configuration);

            IBcryptProvider provider = null;

            provider = new BcryptProvider(configuration);

            return provider;
        }

        private void CheckCreateKey(IConfiguration configuration)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            string fullName = System.IO.Path.Combine(path, "Crypto-GenerateKey.txt");

            if (!System.IO.File.Exists(fullName) && string.IsNullOrEmpty(configuration["Crypt:SecretKey"]) && string.IsNullOrEmpty(configuration["Crypt:SaltingPassword"]))
            {
                System.IO.File.WriteAllText(fullName, JsonConvert.SerializeObject(new
                {
                    GenerateKey = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(this.GeneateStrRandom(32))),
                    SaltingPassword = BCrypt.Net.BCrypt.GenerateSalt(BCrypt.Net.SaltRevision.Revision2B)
                }));
            }

            if (string.IsNullOrEmpty(configuration["Crypt:SecretKey"]))
            {
                throw new NullReferenceException(string.Format("กรุณานำ Key SecretKey ใน folder {0} ไปเพิ่ม Crypt:SecretKey ที่ appsettings.json", fullName));
            }

            if (string.IsNullOrEmpty(configuration["Crypt:SaltingPassword"]))
            {
                throw new NullReferenceException(string.Format("กรุณานำ Key SaltingPassword ใน folder {0} ไปเพิ่ม Crypt:SaltingPassword ที่ appsettings.json", fullName));
            }

            if (System.IO.File.Exists(fullName))
            {
                System.IO.File.Delete(fullName);
            }
        }

        private string GeneateStrRandom(int max)
        {
            return StringExtension.strRandom(max, true);
        }
    }
}
