using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using rs_backends_api_2019.DAL.Models.Defaults.PaginationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace rs_backends_api_2019.DAL.EFExtensions
{
    public static class PaginationExtension
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

        public static Pagination<TEntity> makePaginate<TEntity>(IQueryable<TEntity> queryable, int pageSize = 10, int total = 0, int renderType = 0)
             where TEntity : class
        {
            return queryable.paginate(pageSize, total, renderType);
        }

        public static Pagination<TEntity> makePaginate<TEntity>(IEnumerable<TEntity> queryable, int pageSize = 10, int total = 0, int renderType = 1)
             where TEntity : class
        {
            return queryable.paginate(pageSize, total, renderType);
        }

        public static Pagination<TEntity> makePaginateCustom<TEntity>(IEnumerable<TEntity> queryable, int pageSize = 10, int total = 0)
            where TEntity : class
        {
            return queryable.paginateCustom(pageSize, total);
        }

        // Query DBContext and create pagination object using generic type
        public static Pagination<TEntity> paginate<TEntity>(
            this IQueryable<TEntity> queryable, int pageSize = 10, int total = 0, int renderType = 0) where TEntity : class
        {
            // data to found.
            if (total == 0)
            {
                return new Pagination<TEntity>();
            }

            /*** Start : Get parameter from query string ***/
            string _requestCurrentPage = Current.Request.Query["page"].ToString();
            string _requestPageSize = Current.Request.Query["pageSize"].ToString();

            /*** Start : Get value for process ***/
            Dictionary<string, string> _parameters = getParameters();

            int _pageSize = !string.IsNullOrEmpty(_requestPageSize) ? int.Parse(_requestPageSize) : int.Parse(_configuration["View:PageSize"].ToString());
            var _page = !string.IsNullOrEmpty(_requestCurrentPage) ? int.Parse(_requestCurrentPage) : 1;
            int _total = (int)total;
            int _lastPage = getLastPage(_total, _pageSize);
            int _currentPage = _page;

            string _path = getPath();
            int _from = getFrom(_page, _pageSize);
            int _to = getTo(_page, _pageSize, _total);
            string _firstPageUrl = getPageUrl(1, _currentPage, _path, _parameters);
            string _lastPageUrl = getPageUrl(_lastPage, _currentPage, _path, _parameters);
            string _nextPageUrl = getNextPageUrl(_currentPage, _lastPage, _path, _parameters);
            string _prevPageUrl = getPrevPageUrl(_currentPage, _lastPage, _path, _parameters);
            bool _hasPages = getHasPage(_total, _pageSize);

            int _skip = (_from - 1);
            int _take = (int)_pageSize;

            var query = queryable.AsQueryable();

            if (renderType == 0)
            {
                string _sort = (_parameters != null && _parameters.Keys.Contains("sort")) ? _parameters["sort"] : string.Empty;
                List<string[]> _orders = getIOrdered(_sort);

                // Render order query string
                if (_orders != null && _orders.Count >= 1)
                {
                    var props = typeof(TEntity).GetProperties();

                    foreach (var order in _orders)
                    {
                        var ordered = order[0].ToLower();
                        var find = props.Where(w => ordered.EndsWith(w.Name.ToLower()));

                        if (find != null && find.Count() <= 0)
                        {
                            // continue key not matches.
                            continue;
                        }

                        var x = System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda<TEntity, object>(null, false, order[0].Trim());

                        if (order[1].Trim() == "ASC")
                        {
                            query = query.OrderBy(x);
                        }
                        else
                        {
                            query = query.OrderByDescending(x);
                        }
                    }
                }
            }

            query = query
                .Skip(_skip)
                .Take(_take);

            return new Pagination<TEntity>()
            {
                Total = _total,
                PageSize = _pageSize,
                CurrentPage = _page,
                LastPage = _lastPage,
                Path = _path,
                From = _from,
                To = _to,
                FirstPageUrl = _firstPageUrl,
                LastPageUrl = _lastPageUrl,
                NextPageUrl = _nextPageUrl,
                PrevPageUrl = _prevPageUrl,
                HasPages = _hasPages,
                Data = null,
                Items = query.AsEnumerable()
            };
        }

        // Query for mockup data and create pagination object using generic type
        public static Pagination<TEntity> paginate<TEntity>(
            this IEnumerable<TEntity> queryable, int pageSize = 0, int total = 0, int renderType = 0) where TEntity : class
        {
            // data to found.
            if (total == 0)
            {
                return new Pagination<TEntity>();
            }

            /*** Start : Get parameter from query string ***/
            string _requestCurrentPage = Current.Request.Query["page"].ToString();
            string _requestPageSize = Current.Request.Query["pageSize"].ToString();

            /*** Start : Get value for process ***/
            Dictionary<string, string> _parameters = getParameters();
            string _sort = (_parameters != null && _parameters.Keys.Contains("sort")) ? _parameters["sort"] : string.Empty;
            List<string[]> _orders = getIOrdered(_sort);

            //Set page size.
            int _pageSize = pageSize != 0 ? pageSize : int.Parse(_configuration["View:PageSize"].ToString());
            _pageSize = !string.IsNullOrEmpty(_requestPageSize) ? int.Parse(_requestPageSize) : _pageSize;

            var _page = !string.IsNullOrEmpty(_requestCurrentPage) ? int.Parse(_requestCurrentPage) : 1;
            int _total = (int)total;
            int _lastPage = getLastPage(_total, _pageSize);
            int _currentPage = _page;

            string _path = getPath();
            int _from = getFrom(_page, _pageSize);
            int _to = getTo(_page, _pageSize, _total);
            string _firstPageUrl = getPageUrl(1, _currentPage, _path, _parameters);
            string _lastPageUrl = getPageUrl(_lastPage, _currentPage, _path, _parameters);
            string _nextPageUrl = getNextPageUrl(_currentPage, _lastPage, _path, _parameters);
            string _prevPageUrl = getPrevPageUrl(_currentPage, _lastPage, _path, _parameters);
            bool _hasPages = getHasPage(_total, _pageSize);

            int _skip = (_from - 1);
            int _take = (int)_pageSize;

            var query = queryable;

            if (renderType == 0)
            {
                // Render order query string
                if (_orders != null && _orders.Count >= 1)
                {
                    var props = typeof(TEntity).GetProperties(); ;

                    foreach (var order in _orders)
                    {
                        var ordered = order[0].ToLower();
                        var propIn = props.GetType();

                        var find = props.Where(w => ordered.EndsWith(w.Name.ToLower()));

                        if (find != null && find.Count() <= 0)
                        {
                            // continue key not matches.
                            continue;
                        }

                        var x = System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda<TEntity, object>(null, false, order[0].Trim());
                        if (order[1].Trim() == "ASC")
                        {
                            query = query.OrderBy(x.Compile());
                        }
                        else
                        {
                            query = query.OrderByDescending(x.Compile());
                        }
                    }
                }
            }

            query = query
                .Skip(_skip)
                .Take(_take);

            return new Pagination<TEntity>()
            {
                Total = _total,
                PageSize = _pageSize,
                CurrentPage = _page,
                LastPage = _lastPage,
                Path = _path,
                From = _from,
                To = _to,
                FirstPageUrl = _firstPageUrl,
                LastPageUrl = _lastPageUrl,
                NextPageUrl = _nextPageUrl,
                PrevPageUrl = _prevPageUrl,
                HasPages = _hasPages,
                Data = null,
                Items = query
            };
        }


        // Query for mockup data and create pagination object using generic type
        public static Pagination<TEntity> paginateCustom<TEntity>(
            this IEnumerable<TEntity> items, int pageSize = 0, int total = 0, int renderType = 0) where TEntity : class
        {
            // data to found.
            if (total == 0)
            {
                return new Pagination<TEntity>();
            }

            /*** Start : Get parameter from query string ***/
            string _requestCurrentPage = Current.Request.Query["page"].ToString();
            string _requestPageSize = Current.Request.Query["pageSize"].ToString();

            /*** Start : Get value for process ***/
            Dictionary<string, string> _parameters = getParameters();
            string _sort = (_parameters != null && _parameters.Keys.Contains("sort")) ? _parameters["sort"] : string.Empty;
            List<string[]> _orders = getIOrdered(_sort);

            //Set page size.
            int _pageSize = pageSize != 0 ? pageSize : int.Parse(_configuration["View:PageSize"].ToString());
            _pageSize = !string.IsNullOrEmpty(_requestPageSize) ? int.Parse(_requestPageSize) : _pageSize;

            var _page = !string.IsNullOrEmpty(_requestCurrentPage) ? int.Parse(_requestCurrentPage) : 1;
            int _total = (int)total;
            int _lastPage = getLastPage(_total, _pageSize);
            int _currentPage = _page;

            string _path = getPath();
            int _from = getFrom(_page, _pageSize);
            int _to = getTo(_page, _pageSize, _total);
            string _firstPageUrl = getPageUrl(1, _currentPage, _path, _parameters);
            string _lastPageUrl = getPageUrl(_lastPage, _currentPage, _path, _parameters);
            string _nextPageUrl = getNextPageUrl(_currentPage, _lastPage, _path, _parameters);
            string _prevPageUrl = getPrevPageUrl(_currentPage, _lastPage, _path, _parameters);
            bool _hasPages = getHasPage(_total, _pageSize);

            return new Pagination<TEntity>()
            {
                Total = _total,
                PageSize = _pageSize,
                CurrentPage = _page,
                LastPage = _lastPage,
                Path = _path,
                From = _from,
                To = _to,
                FirstPageUrl = _firstPageUrl,
                LastPageUrl = _lastPageUrl,
                NextPageUrl = _nextPageUrl,
                PrevPageUrl = _prevPageUrl,
                HasPages = _hasPages,
                Data = null,
                Items = items
            };
        }

        // Casting genenic type to data for converter DBContext to my Model
        public static Pagination<T> paginateCasting<T, TResult>(this Pagination<T> pagination, Func<T, TResult> predicate) where T : class where TResult : class
        {
            return new Pagination<T>
            {
                Total = pagination.Total,
                PageSize = pagination.PageSize,
                CurrentPage = pagination.CurrentPage,
                LastPage = pagination.LastPage,
                Path = pagination.Path,
                From = pagination.From,
                To = pagination.To,
                FirstPageUrl = pagination.FirstPageUrl,
                LastPageUrl = pagination.LastPageUrl,
                NextPageUrl = pagination.NextPageUrl,
                PrevPageUrl = pagination.PrevPageUrl,
                HasPages = pagination.HasPages,
                Data = null,
                Items = ((IEnumerable<T>)pagination.Items).Select(predicate)
            };
        }

        // Get parameter from query string
        public static Dictionary<string, string> getParameters()
        {
            return Current.Request.Query.ToDictionary(c => c.Key, v => v.Value.ToString());
        }

        // Compile SQL Command for Order By with URL
        // Example : {url}?sort=id_asc <= for order by asc
        // Example multiple order : {url}?sort=id_asc,name_desc,..,n <= for multiple order
        public static List<string[]> getIOrdered(string sort)
        {
            if (string.IsNullOrEmpty(sort) || sort == "-")
            {
                return null;
            }

            List<string[]> orders = new List<string[]>();
            string[] orderAsc = new string[] { "_asc", "_Asc", "_ASC" };
            string[] orderDesc = new string[] { "_desc", "_Desc", "_DESC" };

            string[] sorts = sort.Split(',').ToArray();

            if (sorts != null && sorts.Length >= 1)
            {
                orders = sorts.Select(s =>
                {
                    string order = s.Trim();
                    bool IsAsc = orderAsc.Any(a => order.EndsWith(a));
                    bool IsDesc = orderDesc.Any(a => order.EndsWith(a));

                    if (!IsAsc && !IsDesc)
                    {
                        return null;
                    }

                    order = Regex.Replace(order, "(_asc)|(_desc)", string.Empty, RegexOptions.IgnoreCase);

                    string expression = $"order => order.{order}|{(IsAsc && !IsDesc ? "ASC" : "DESC")}";

                    return expression.Trim().Split('|');
                }).ToList();
            }

            // Filter ordered length more than equals 2 { "order => order.Name", "ASC" }
            return orders.Where(w => w != null && w.Length >= 2).ToList();
        }

        // Get last page number
        public static int getLastPage(int total, int pageSize)
        {
            if (pageSize == 0) return 0;

            return (int)(Math.Ceiling((double)total / (double)pageSize));
        }

        // Get path for generate path pagination
        public static string getPath(string CustomPath = "")
        {
            if (!string.IsNullOrEmpty(CustomPath))
            {
                return CustomPath;
            }
            return string.Empty;
            //return $"{Current.Request.Scheme}://{Current.Request.Host}{Current.Request.PathBase}{Current.Request.Path}";
        }

        // Get number start item of page
        public static int getFrom(int page, int pageSize)
        {
            if (page == 1)
            {
                return page;
            }

            var count = ((pageSize * page) + 1) - pageSize;

            return count;
        }

        // Get number stop item of page
        public static int getTo(int page, int pageSize, int total)
        {
            int count = 0;
            if (page == 1)
            {
                count = (pageSize >= total) ? total : pageSize;
                return count;
            }

            count = (pageSize * page) + 0;

            if (count > total)
            {
                count = (count - (count - total));
            }

            return count;
        }

        // Get url from page number
        public static string getPageUrl(int page, int currentPage, string path, Dictionary<string, string> parameter = null)
        {
            if (page == currentPage)
            {
                return null;
            }

            string firstUrl = path;

            var clone = parameter;

            clone["page"] = page.ToString();

            string queryString = getQueryString(clone);

            return string.Concat(firstUrl, queryString);
        }

        // Convert Dictionary to query string
        public static string getQueryString(Dictionary<string, string> query)
        {
            string queryString = string.Empty;

            foreach (var q in query)
            {
                var key = q.Key.ToString();
                var value = q.Value != null ? q.Value.ToString() : string.Empty;
                queryString += key + "=" + value + "&";
            }

            queryString = queryString.Trim('&');
            queryString = (!string.IsNullOrEmpty(queryString)) ? string.Concat("?", queryString.Trim()) : string.Empty;

            return queryString;
        }

        // Get url next page
        public static string getNextPageUrl(int currentPage, int lastPage, string path, Dictionary<string, string> parameters = null)
        {
            int? page = getNext(currentPage, lastPage);

            if (page == null)
            {
                return null;
            }

            return getPageUrl((int)page, currentPage, path, parameters);
        }

        // Get next page number
        public static int? getNext(int currentPage, int lastPage)
        {
            var page = currentPage + 1;

            if (page > lastPage)
            {
                return null;
            }

            return page;
        }

        // Get url prev page
        public static string getPrevPageUrl(int currentPage, int lastPage, string path, Dictionary<string, string> parameters = null)
        {
            try
            {
                int? page = getPrev(currentPage);

                if (page == null)
                {
                    return null;
                }

                return getPageUrl((int)page, currentPage, path, parameters);
            }
            catch (Exception e)
            {
                return getPageUrl(1, 1, path, parameters);
            }
        }

        // Get prev page number
        public static int? getPrev(int currentPage)
        {
            int? page = currentPage - 1;

            if (page < 1)
            {
                page = null;
            }

            if (page == 0)
            {
                page = 1;
            }

            return page;
        }

        // Get value has long page
        public static bool getHasPage(int total, int pageSize)
        {
            return (total > pageSize);
        }

        // Create default data
        private static object createDefaultObject(object obj)
        {
            List<Type> genericTypeArguments = new List<Type>();
            var props = obj.GetType();
            var baseType = props.BaseType.FullName;

            Type instance = Type.GetType(baseType);
            var instanceObject = Activator.CreateInstance(instance);
            var propsNewInstanceObject = instanceObject.GetType().GetProperties();

            foreach (var prop in propsNewInstanceObject)
            {
                if (prop.CanWrite)
                {
                    string propName = prop.Name;
                    Type propertyType = prop.PropertyType;

                    object value = defaultForType(propertyType);
                    prop.SetValue(instanceObject, value, null);
                }
            }

            return instanceObject;
        }

        // Support createDefaultObject
        private static object defaultForType(Type targetType)
        {
            if (!targetType.IsValueType && targetType == typeof(string))
            {
                return string.Empty;
            }
            else if (targetType.IsValueType)
            {
                return Activator.CreateInstance(targetType);
            }
            return Activator.CreateInstance(targetType);
        }
    }
}
