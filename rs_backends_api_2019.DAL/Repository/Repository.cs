using Microsoft.EntityFrameworkCore;
using rs_backends_api_2019.DAL.EFExtensions;
using rs_backends_api_2019.DAL.Interfaces;
using rs_backends_api_2019.DAL.Models.Defaults.PaginationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace rs_backends_api_2019.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        protected IQueryable<TEntity> queryable { get; set; }
        protected string sqlCommand;

        public Repository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<TEntity>();
            this.queryable = this._dbSet;
        }

        #region -- Start : Query for select
        [Obsolete("Method is replaced by Select custom field")]
        public IEnumerable<TResult> select<TResult>(Expression<Func<TEntity, TResult>> predicate)
        {
            var response = this.queryable.Select<TEntity, TResult>(predicate).AsEnumerable().ToList();

            this.queryable = this._dbSet;
            return response;
        }

        [Obsolete("Method is replaced by Count data")]
        public int count(Expression<Func<TEntity, bool>> predicate = null)
        {
            int response = 0;

            if (predicate == null)
            {
                response = this.queryable.Count();
            }
            else
            {
                response = this.queryable.Count(predicate);
            }

            this.queryable = this._dbSet;
            return response;
        }

        [Obsolete("Method is replaced by Select get first item")]
        public TEntity first(Expression<Func<TEntity, bool>> predicate = null)
        {
            TEntity response = null;

            if (predicate == null)
            {
                response = this.queryable.FirstOrDefault();
            }
            else
            {
                response = this.queryable.FirstOrDefault(predicate);
            }

            this.queryable = this._dbSet;
            return response;
        }

        [Obsolete("Method is replaced by Select get last item")]
        public TEntity last(Expression<Func<TEntity, bool>> predicate = null)
        {
            TEntity response = null;

            if (predicate == null)
            {
                response = this.queryable.LastOrDefault();
            }
            else
            {
                response = this.queryable.LastOrDefault(predicate);
            }

            this.queryable = this._dbSet;
            return response;
        }

        [Obsolete("Method is replaced by GetList")]
        public IEnumerable<TEntity> get()
        {
            var response = this.queryable.AsEnumerable().ToList();

            this.queryable = this._dbSet;

            return response;
        }

        [Obsolete("Method is replaced by Get Lists and convert to pagination")]
        public Pagination<TEntity> paginate(int pageSize = 10)
        {
            // get total record
            var total = this.queryable.Count();

            var response = this.queryable.paginate(pageSize, total);

            this.queryable = this._dbSet;
            return response;
        }

        [Obsolete("Method is replaced by Get Lists and convert to pagination and convert casting for custom type of data")]
        public Pagination<TEntity> paginate<TResult>(Func<TEntity, TResult> predicate, int pageSize = 10) where TResult : class
        {
            // get total record
            var total = this.queryable.Count();

            var response = this.queryable.paginate(pageSize, total)
                .paginateCasting<TEntity, TResult>(predicate); // Casting for convert type

            this.queryable = this._dbSet;
            return response;
        }

        [Obsolete("Method is replaced by Get from sql raw")]
        public IEnumerable<TEntity> fromSql(string cmd)
        {
            return _dbSet.FromSql(cmd).AsEnumerable().ToList();
        }
        #endregion -- Stop : Query for select

        #region -- Start : Condition of Clouse
        [Obsolete("Method is replaced by Where clouse")]
        public IRepository<TEntity> where(Expression<Func<TEntity, bool>> predicate)
        {
            this.queryable = this.queryable.Where(predicate);

            return this;
        }
        #endregion -- Stop : Condition of Clouse

        #region -- Start : Ordered Item
        [Obsolete("Method is replaced by Sort data ASC")]
        public IRepository<TEntity> orderBy<TKey>(Expression<Func<TEntity, TKey>> predicate)
        {
            this.queryable = this.queryable.OrderBy(predicate);

            return this;
        }

        [Obsolete("Method is replaced by Sort data DESC")]
        public IRepository<TEntity> orderByDescending<TKey>(Expression<Func<TEntity, TKey>> predicate)
        {
            this.queryable = this.queryable.OrderByDescending(predicate); //.OrderByDescending(predicate);

            return this;
        }

        [Obsolete("Method is replaced by Sort data DESC")]
        public IRepository<TEntity> autoSort(params string[] columns)
        {
            this.queryable = this.queryable.AutoSort(columns);

            return this;
        }
        #endregion -- Stop : Ordered Item

        #region -- Start : Limit or Offset
        [Obsolete("Method is replaced by Limit data")]
        public IRepository<TEntity> take(int take)
        {
            this.queryable = this.queryable.Take(take);

            return this;
        }

        [Obsolete("Method is replaced by Offset")]
        public IRepository<TEntity> skip(int skip)
        {
            this.queryable = this.queryable.Skip(skip);

            return this;
        }
        #endregion -- Stop : Limit or Offset

        #region -- Start : Insert Data
        public void add(TEntity entity) => _dbSet.Add(entity);

        public void addRange(params TEntity[] entities) => _dbSet.AddRange(entities);

        public void addRange(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);
        #endregion -- Stop : Insert Data

        #region -- Start : Update Data
        public void update(TEntity entity) => _dbSet.Update(entity);

        public void update(params TEntity[] entities) => _dbSet.UpdateRange(entities);

        public void update(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);
        #endregion -- Stop : Update Data

        #region -- Start : Delete Data
        public void delete(TEntity entity) => _dbSet.Remove(entity);

        public void delete(params TEntity[] entities) => _dbSet.RemoveRange(entities);

        public void delete(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);
        #endregion -- Stop : Delete Data

        #region -- Start : RelationShip
        public void include(params Expression<Func<TEntity, object>>[] predicate)
        {
            foreach (var pre in predicate)
            {
                _dbSet.Include(pre).Load();
            }
        }
        #endregion -- Stop : RelationShip

        #region -- Start : Implement Linq to Quries
        public IQueryable<TEntity> asQueryable()
        {
            return this.queryable;
        }
        #endregion -- Stop

        #region -- Start : Debug
        [Obsolete("Method is replaced by Get sql generate script")]
        public string toSql()
        {
            return this.queryable.ToSql();
        }
        #endregion -- Stop : Debug
    }
}
