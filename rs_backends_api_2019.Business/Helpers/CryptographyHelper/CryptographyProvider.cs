using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using rs_backends_api_2019.Business.Helpers.CryptographyHelper.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace rs_backends_api_2019.Business.Helpers.CryptographyHelper
{
    public class CryptographyProvider : ICryptographyProvider
    {
        private string _secretKey { get; set; }

        private readonly IConfiguration _configuration;

        public CryptographyProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _secretKey = _configuration["Crypt:SecretKey"];
        }

        public string Decrypt(string cipherText)
        {
            dynamic payload = this.GetJsonPayload(cipherText);

            RijndaelManaged aes = new RijndaelManaged();

            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;

            aes.Key = Convert.FromBase64String(this._secretKey);
            aes.IV = Convert.FromBase64String(payload["iv"]);

            ICryptoTransform AESDecrypt = aes.CreateDecryptor(aes.Key, aes.IV);

            byte[] buffer = Convert.FromBase64String(payload["value"]);

            return (Encoding.UTF8.GetString(AESDecrypt.TransformFinalBlock(buffer, 0, buffer.Length))).ToString();
        }

        public string Encrypt(string plainText)
        {
            RijndaelManaged aes = new RijndaelManaged();

            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;

            aes.Key = Convert.FromBase64String(_secretKey);
            aes.GenerateIV();

            ICryptoTransform AESEncrypt = aes.CreateEncryptor(aes.Key, aes.IV);

            byte[] buffer = Encoding.UTF8.GetBytes(plainText);

            string encryptedText = Convert.ToBase64String(AESEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));

            string mac = string.Empty;

            using (var hmacsha256 = new HMACSHA256(Convert.FromBase64String(this._secretKey)))
            {
                hmacsha256.ComputeHash(Encoding.Default.GetBytes(Convert.ToBase64String(aes.IV) + encryptedText));
                mac = this.ByteToString(hmacsha256.Hash);
            }

            return JsonPayload(aes.IV, encryptedText, mac);
        }

        private dynamic GetJsonPayload(string encryptedText)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(Encoding.UTF8.GetString(Convert.FromBase64String(encryptedText)));
        }

        private dynamic JsonPayload(byte[] iv, string encryptedText, string mac)
        {
            var keyValues = new Dictionary<string, object>
                {
                    { "iv", Convert.ToBase64String(iv) },
                    { "value", encryptedText },
                    { "mac", mac },
                };

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(keyValues)));
        }

        private string ByteToString(byte[] buff)
        {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++)
                sbinary += buff[i].ToString("x2"); /* hex format */
            return sbinary;

        }
    }
}
