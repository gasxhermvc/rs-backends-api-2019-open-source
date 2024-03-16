using System;
using System.Collections.Generic;
using System.Text;

namespace rs_backends_api_2019.Business.Extensions
{
    public static class BinaryExtension
    {
        //=>for encode
        public static string base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        //=>for decode
        public static string base64Decode(this string plainText)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(plainText);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
