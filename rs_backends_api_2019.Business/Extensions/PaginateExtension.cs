using rs_backends_api_2019.DAL.EFExtensions;
using rs_backends_api_2019.DAL.Models.Defaults.PaginationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rs_backends_api_2019.Business.Extensions
{
    public static class PaginateExtension
    {
        // Query DBContext and create pagination object using generic type
        public static Pagination<TEntity> paginate<TEntity>(this IQueryable<TEntity> queryable, int pageSize = 10) where TEntity : class
        {
            var count = queryable.Count();

            return PaginationExtension.makePaginate(queryable, pageSize, count, 1);
        }

        // Query DBContext and create pagination object using generic type
        public static Pagination<TEntity> paginate<TEntity>(this IEnumerable<TEntity> queryable, int pageSize = 10) where TEntity : class
        {
            var count = queryable.Count();

            return PaginationExtension.makePaginate(queryable, pageSize, count, 1);
        }

        // Query DBContext and create pagination object using generic type with fixed total item
        public static Pagination<TEntity> paginateCustom<TEntity>(this IEnumerable<TEntity> queryable, int pageSize = 10, int totalItem = 0) where TEntity : class
        {
            var count = totalItem != 0 ? totalItem : queryable.Count();

            return PaginationExtension.makePaginateCustom(queryable, pageSize, count);
        }


        // Casting genenic type to data for converter DBContext to my Model
        public static Pagination<T> paginate<T, TResult>(this Pagination<T> pagination, Func<T, TResult> predicate) where T : class where TResult : class
        {
            return pagination.paginateCasting(predicate);
        }

        public static IQueryable<TEntity> autoSort<TEntity>(this IQueryable<TEntity> queryable, string[] columns = null) where TEntity : class
        {
            return queryable.AutoSort(columns);
        }
    }
}
