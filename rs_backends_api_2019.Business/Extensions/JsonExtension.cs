using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace rs_backends_api_2019.Business.Extensions
{
    public static class JsonExtension
    {
        public static T JsonDecode<T>(this string input) where T : class
        {
            T result = JsonConvert.DeserializeObject<T>(Regex.Unescape(input));

            return result;
        }

        public static string JsonEncode(object data)
        {
            string result = Regex.Escape(JsonConvert.SerializeObject(data));

            return result;
        }
    }
}
