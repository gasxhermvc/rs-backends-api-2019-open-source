using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace rs_backends_api_2019.DAL.EFExtensions
{
    public static class PaginationOrderedExtension
    {
        // Get application config (appsetting.json)
        private static IConfiguration _configuration { get; set; }

        // Get http context
        private static IHttpContextAccessor _httpContext { get; set; }

        // Get client request or http headers
        private static HttpContext Current
        {
            get => _httpContext.HttpContext;
        }

        // Get injection http context and config from startup.cs
        public static void configure(IHttpContextAccessor httpContext, IConfiguration configuration)
        {
            _httpContext = httpContext;

            _configuration = configuration;
        }

        public static IQueryable<TEntity> AutoSort<TEntity>(this IQueryable<TEntity> queryable, params string[] orderColumn) where TEntity : class
        {
            Dictionary<string, string> _parameters = PaginationExtension.getParameters();
            string _sort = (_parameters != null && _parameters.Keys.Contains("sort")) ? _parameters["sort"] : string.Empty;
            List<string[]> _orders = PaginationExtension.getIOrdered(_sort);

            if (!string.IsNullOrEmpty(_sort))
            {
                // Render order query string
                if (_orders != null && _orders.Count >= 1)
                {
                    var props = new PropertyInfo[] { };
                    if (orderColumn != null && orderColumn.Length >= 1)
                    {
                        var columns = orderColumn.Select(s => s.ToLower().Trim()).ToList();
                        props = typeof(TEntity).GetProperties().Where(w => columns.Contains(w.Name.ToLower())).ToArray();
                    }
                    else
                    {
                        props = typeof(TEntity).GetProperties();
                    }

                    if (props != null && props.Length >= 1)
                    {
                        foreach (var order in _orders)
                        {
                            var ordered = order[0].ToLower();
                            var propIn = props.GetType();

                            var find = props.Where(w => ordered.EndsWith(w.Name.ToLower()) && ordered.Replace("order => order.", string.Empty).ToLower() == w.Name.ToLower()).FirstOrDefault();

                            if (find == null)
                            {
                                // continue key not matches.
                                continue;
                            }

                            var x = System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda<TEntity, object>(null, false, order[0].Trim());

                            if (order[1].Trim() == "ASC")
                            {
                                queryable = queryable.OrderBy(x);
                                break;
                            }
                            else
                            {
                                queryable = queryable.OrderByDescending(x);
                                break;
                            }
                        }
                    }
                }

            }
            return queryable;
        }
    }
}
