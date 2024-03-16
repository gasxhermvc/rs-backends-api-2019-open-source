using rs_backends_api_2019.DAL.Models.Defaults.PaginationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace rs_backends_api_2019.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // IQueryable<TEntity> queryable { get; set; }

        #region -- Start : Query for select
        IEnumerable<TResult> select<TResult>(Expression<Func<TEntity, TResult>> predicate);

        int count(Expression<Func<TEntity, bool>> predicate = null);

        TEntity first(Expression<Func<TEntity, bool>> predicate = null);

        TEntity last(Expression<Func<TEntity, bool>> predicate = null);

        Pagination<TEntity> paginate(int pageSize = 10);

        Pagination<TEntity> paginate<TResult>(Func<TEntity, TResult> predicate, int pageSize = 10) where TResult : class;

        IEnumerable<TEntity> get();

        IEnumerable<TEntity> fromSql(string cmd);
        #endregion -- Stop : Query for select

        #region -- Start : Condition of Clouse
        IRepository<TEntity> where(Expression<Func<TEntity, bool>> predicate);
        #endregion -- Stop : Condition of Clouse

        #region -- Start : Ordered Item
        IRepository<TEntity> orderBy<TKey>(Expression<Func<TEntity, TKey>> predicate);

        IRepository<TEntity> orderByDescending<TKey>(Expression<Func<TEntity, TKey>> predicate);

        IRepository<TEntity> autoSort(params string[] columns);
        #endregion -- Stop : Ordered Item

        #region -- Start : Limit or Offset
        IRepository<TEntity> take(int take);

        IRepository<TEntity> skip(int skip);
        #endregion -- Stop : Limit or Offset

        #region -- Start : Insert Data
        void add(TEntity entity);

        void addRange(params TEntity[] entities);

        void addRange(IEnumerable<TEntity> entities);
        #endregion -- Stop : Insert Data

        #region -- Start : Update Data
        void update(TEntity entity);

        void update(params TEntity[] entities);

        void update(IEnumerable<TEntity> entities);
        #endregion -- Stop : Update Data

        #region -- Start : Delete Data
        void delete(TEntity entity);

        void delete(params TEntity[] entities);

        void delete(IEnumerable<TEntity> entities);
        #endregion -- Stop : Delete Data

        #region -- Start : RelationShip
        void include(params Expression<Func<TEntity, object>>[] predicate);
        #endregion -- Stop : RelationShip

        #region -- Start : Implement Linq to Queries
        IQueryable<TEntity> asQueryable();
        #endregion -- Stop : Implement Linq to Queries

        #region --Start : Debug
        string toSql();
        #endregion --Stop : Debug
    }
}
