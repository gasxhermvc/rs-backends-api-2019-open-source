using rs_backends_api_2019.DAL.EFExtensions;
using rs_backends_api_2019.DAL.Models.Defaults.PaginationModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace rs_backends_api_2019.Business.Extensions
{
    public static class ObjectExtension
    {
        // แปลงค่าตัวอักษรไปเป็นเลขจำนวนเต็มแบบ int
        public static int ToInt(this string integer)
        {
            return int.Parse(integer);
        }

        public static float ToFloat(this object integer)
        {
            return float.Parse(integer.ToString());
        }

        // แปลงค่าตัวอักษรไปเป็นเลขจำนวนเต็มแบบ long
        public static long ToLongInt(this string integer)
        {
            return long.Parse(integer);
        }
      
        public static IDictionary<string, string> ToKeyValue(this object metaToken)
        {
            if (metaToken == null)
            {
                return null;
            }

            JToken token = metaToken as JToken;
            if (token == null)
            {
                return ToKeyValue(JObject.FromObject(metaToken));
            }

            if (token.HasValues)
            {
                var contentData = new Dictionary<string, string>();
                foreach (var child in token.Children().ToList())
                {
                    var childContent = child.ToKeyValue();
                    if (childContent != null)
                    {
                        contentData = contentData.Concat(childContent)
                            .ToDictionary(k => k.Key, v => v.Value);
                    }
                }

                return contentData;
            }

            var jValue = token as JValue;
            if (jValue?.Value == null)
            {
                return null;
            }

            var value = jValue?.Type == JTokenType.Date ?
                jValue?.ToString("o", CultureInfo.InvariantCulture) :
                jValue?.ToString(CultureInfo.InvariantCulture);

            return new Dictionary<string, string> { { token.Path, value } };
        }
    }
}
