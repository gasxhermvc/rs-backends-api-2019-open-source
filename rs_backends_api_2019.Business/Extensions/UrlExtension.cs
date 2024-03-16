using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace rs_backends_api_2019.Business.Extensions
{
    public static class UrlExtension
    {
        private static IHttpContextAccessor m_httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            m_httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext Current
        {
            get
            {
                return m_httpContextAccessor.HttpContext;
            }
        }

        public static string BaseUrl()
        {
            var context = Current;

            string baseUrl = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase;

            return baseUrl;
        }

        public static string PathUrl(string queryString = "")
        {
            var context = Current;

            string baseUrl = BaseUrl() + context.Request.Path;

            return baseUrl + (!string.IsNullOrEmpty(queryString) ? "?" + queryString : string.Empty);
        }

        public static string RefererUrl(string queryString = "")
        {
            var context = Current;

            string refererUrl = context.Request.Headers["Referer"];

            return refererUrl + (!string.IsNullOrEmpty(queryString) ? "?" + queryString : string.Empty);
        }

        public static string GetQuery(string key, string defaultValue = "")
        {
            var queryString = Current.Request.Query.ToDictionary(c => c.Key, v => v.Value.ToString());
            if (queryString.ContainsKey(key))
            {
                return queryString.Where(s => s.Key == key).FirstOrDefault().Value.ToString();
            }

            return defaultValue;
        }

        public static Dictionary<string, string> GetParameterWithDictionary(Dictionary<string, string> values = null)
        {
            var queryString = Current.Request.Query.ToDictionary(c => c.Key, v => v.Value.ToString());

            if (values != null && values.Count >= 1)
            {
                foreach (var obj in values)
                {
                    if (!queryString.Keys.Contains(obj.Key))
                    {
                        queryString.Add(obj.Key, obj.Value.ToString());
                    }
                    else
                    {
                        queryString[obj.Key] = obj.Value.ToString();
                    }
                }
            }

            return Current.Request.Query.ToDictionary(c => c.Key, v => v.Value.ToString());
        }

        public static Dictionary<string, string> CompactParameterToDictionary(object values = null)
        {
            var queryString = Current.Request.Query.ToDictionary(c => c.Key, v => v.Value.ToString());

            if (values != null)
            {
                foreach (PropertyInfo prop in values.GetType().GetProperties())
                {
                    var propName = prop.Name.ToString();

                    if (!queryString.Keys.Contains(propName))
                    {
                        queryString.Add(propName, prop.GetValue(values)?.ToString());
                    }
                    else
                    {
                        queryString[propName] = prop.GetValue(values)?.ToString();
                    }
                }
            }

            return queryString;
        }

        public static IDictionary<string, string> CompactParameterToIDictionary(object values = null)
        {
            var queryString = Current.Request.Query.ToDictionary(c => c.Key, v => v.Value.ToString());

            if (values != null)
            {
                foreach (PropertyInfo prop in values.GetType().GetProperties())
                {
                    var propName = prop.Name.ToString();

                    if (!queryString.Keys.Contains(propName))
                    {
                        queryString.Add(propName, prop.GetValue(values, null)?.ToString());
                    }
                    else
                    {
                        queryString[propName] = prop.GetValue(values, null)?.ToString();
                    }
                }
            }

            return queryString;
        }

        public static string toQueryString(object values = null)
        {
            var value = values.ToKeyValue();
            var queryString = "?";
            int count = value.Count();
            var index = 0;
            foreach (var item in value)
            {
                queryString += item.Key.ToString() + "=" + item.Value.ToString() + "&";

                index++;
            }
            queryString = queryString.Trim('&');
            return queryString;
        }

        public static string GetQueryString()
        {
            return Current.Request.QueryString.ToString();
        }

        public static bool IsLocal()
        {
            string host = Current.Request.Host.ToString().ToLower();
            Console.WriteLine("HOST : {0}", host);
            return (host.Contains("localhost"));
        }
    }
}
