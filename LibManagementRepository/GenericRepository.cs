
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LibManagementModel;
using Microsoft.EntityFrameworkCore;

namespace LibManagementRepository
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Private member variables...
        /// <summary>
        /// Database context
        /// </summary>
        internal DbContext Context;

        /// <summary>
        /// Current database set
        /// </summary>
        internal DbSet<TEntity> DbSet;
        #endregion

        #region Public Constructor...
        /// <summary>
        /// Initializes a new instance of the <see cref="{TEntity}" /> class.
        /// </summary>
        /// <param name="context">Database context</param>
        public GenericRepository(LibManagementContext context)
        {
            this.Context = context;
            this.DbSet = Context.Set<TEntity>();
        }
        #endregion

        #region Public member methods...

        /// <summary>
        /// generic Get method for Entities
        /// </summary>
        /// <returns>List of entities</returns>
        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = this.DbSet;
            return query.ToList();
        }

        /// <summary>
        /// Generic get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id">ID column</param>
        /// <returns>single T result</returns>
        public virtual TEntity GetByID(object id)
        {
            return this.DbSet.Find(id);
        }

        /// <summary>
        /// generic Insert method for the entities
        /// </summary>
        /// <param name="entity">Entity object</param>
        public virtual void Insert(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="id">ID column</param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = this.DbSet.Find(id);
            this.Delete(entityToDelete);
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entityToDelete">Entity object</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (this.Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.DbSet.Attach(entityToDelete);
            }

            this.DbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entityToUpdate">Entity object</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            this.DbSet.Attach(entityToUpdate);
            this.Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual void AddOrupdate(TEntity entityToAddorUpdate)
        {
            //this.DbSet.AddOrUpdate(entityToAddorUpdate);
        }

        /// <summary>
        /// generic method to get many record on the basis of a condition.
        /// </summary>
        /// <param name="where">Criteria to match on</param>
        /// <returns>List of records that matches the specified criteria</returns>
        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return this.DbSet.Where(where).ToList();
        }

        /// <summary>
        /// generic method to get many record on the basis of a condition but query able.
        /// </summary>
        /// <param name="where">Criteria to match on</param>
        /// <returns>List of records that matches the specified criteria</returns>
        public virtual IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            return this.DbSet.Where(where).AsQueryable();
        }

        /// <summary>
        /// generic get method , fetches data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where">Criteria to match on</param>
        /// <returns>A record that matches the specified criteria</returns>
        public TEntity Get(Func<TEntity, bool> where)
        {
            return this.DbSet.Where(where).FirstOrDefault<TEntity>();
        }

        /// <summary>
        /// generic delete method , deletes data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where">Criteria to match on</param>       
        public void Delete(Func<TEntity, bool> where)
        {
            IQueryable<TEntity> objects = this.DbSet.Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
            {
                this.DbSet.Remove(obj);
            }
        }

        /// <summary>
        /// generic method to fetch all the records from database
        /// </summary>
        /// <returns>List of records</returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.DbSet.ToList();
        }

        /// <summary>
        ///  Include  multiple
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <param name="include">include Entity separated by comma</param>
        /// <returns>List of records that matches the specified criteria</returns>
        public IQueryable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        /// <summary>
        /// Include  multiple
        /// </summary>
        /// <param name="include">include Entity separated by comma</param>
        /// <returns>List of records that included</returns>
        public IQueryable<TEntity> GetWithInclude(params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query;
        }

        /// <summary>
        /// Generic method to check if entity exists
        /// </summary>
        /// <param name="primaryKey">Key object</param>
        /// <returns>Return true or false</returns>
        public bool Exists(object primaryKey)
        {
            return this.DbSet.Find(primaryKey) != null;
        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return this.DbSet.Single<TEntity>(predicate);
        }

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return this.DbSet.First<TEntity>(predicate);
        }

        /// <summary>
        /// To do pagination with predicate
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <param name="pageIndex">Index of the page</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="totalItemCount">Total Row Counts</param>
        /// <returns>List of records that matches the specified criteria</returns>
        public IQueryable<TEntity> ToPagedList(Func<TEntity, bool> predicate, int? pageIndex, int pageSize, out int totalItemCount)
        {
            totalItemCount = 0;
            IQueryable<TEntity> items = this.DbSet.Where(predicate).AsQueryable();
            if (items == null)
            {
                return null;
            }

            var truePageIndex = pageIndex ?? 0;
            var itemIndex = truePageIndex - 1;
            if (itemIndex == -1)
                itemIndex = 0;
            pageSize = pageSize - itemIndex;
            var pageOfItems = items.Skip(itemIndex).Take(pageSize);
            totalItemCount = items.Count();
            return pageOfItems;
        }

        /// <summary>
        /// To do pagination with predicate
        /// </summary>
        /// <param name="pageIndex">Index of the page</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="totalItemCount">Total Row Counts</param>
        /// <returns>List of records that matches the specified criteria</returns>
        public IQueryable<TEntity> ToPagedList(int? pageIndex, int pageSize, out int totalItemCount)
        {
            IQueryable<TEntity> queryable = this.DbSet.AsQueryable();
            totalItemCount = 0;
            if (queryable == null)
            {
                return null;
            }

            var truePageIndex = pageIndex ?? 0;
            var itemIndex = truePageIndex - 1;
            if (itemIndex == -1)
                itemIndex = 0;
            pageSize = pageSize - itemIndex;
            var pageOfItems = queryable.Skip(itemIndex).Take(pageSize);
            totalItemCount = queryable.Count();
            return pageOfItems;
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, TEntity> columns)
        {
            return this.DbSet.Select<TEntity, TEntity>(columns).ToList();
        }

        public U Get<U>(Expression<Func<TEntity, bool>> exp, Expression<Func<TEntity, U>> columns)
                where U : class
        {
            return this.DbSet.Where(exp).Select<TEntity, U>(columns).FirstOrDefault();
        }

        public IEnumerable<U> GetMany<U>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, U>> columns) where U : class
        {
            return this.DbSet.Where(where).Select<TEntity, U>(columns).ToList();
        }
        #endregion       
    }
}
