using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibManagementRepository
{
   
    /// <summary>
    /// Interface Repository
    /// </summary>
    /// <typeparam name="T">Type of Entity</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get Single Entity by Id
        /// </summary>
        /// <param name="id">Identity column</param>
        /// <returns>Single Entity</returns>
        T GetByID(object id);

        /// <summary>
        /// Insert Single Entity
        /// </summary>
        /// <param name="entity">Single Entity</param>
        void Insert(T entity);

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id">Identity column</param>
        void Delete(object id);

        /// <summary>
        /// Delete by entity
        /// </summary>
        /// <param name="entityToDelete">Single Entity</param>
        void Delete(T entityToDelete);

        /// <summary>
        /// Update by entity
        /// </summary>
        /// <param name="entityToUpdate">Single Entity</param>
        void Update(T entityToUpdate);
        void AddOrupdate(T entityToAddorUpdate);

        /// <summary>
        /// Get Enumerable records
        /// </summary>
        /// <param name="where">Conditions</param>
        /// <returns>IEnumerable values</returns>
        IEnumerable<T> GetMany(Func<T, bool> where);

        /// <summary>
        /// Get many returning by Queryable
        /// </summary>
        /// <param name="where">Conditions</param>
        /// <returns>Queryable values</returns>
        IQueryable<T> GetManyQueryable(Func<T, bool> where);

        /// <summary>
        /// Get single record
        /// </summary>
        /// <param name="where">Conditions</param>
        /// <returns>Entity</returns>
        T Get(Func<T, bool> where);

        /// <summary>
        /// Delete the record based on conditions
        /// </summary>
        /// <param name="where">Conditions</param>
        void Delete(Func<T, bool> where);

        /// <summary>
        /// Retrieve all records
        /// </summary>
        /// <returns>Enumerable items</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get nested table records
        /// </summary>
        /// <param name="predicate">Conditions</param>
        /// <param name="include">Entity seperated by comma</param>
        /// <returns>Queryable values</returns>
        IQueryable<T> GetWithInclude(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] include);

        /// <summary>
        /// Check whether the key is exists or not
        /// </summary>
        /// <param name="primaryKey">Primary key value</param>
        /// <returns>True or false </returns>
        bool Exists(object primaryKey);


        /// <summary>
        /// Get single record
        /// </summary>
        /// <param name="predicate">Conditions</param>
        /// <returns>Single record Entity</returns>
        T GetSingle(Func<T, bool> predicate);

        /// <summary>
        /// Retrieve first record alone
        /// </summary>
        /// <param name="predicate">Conditions</param>
        /// <returns>Single record Entity</returns>
        T GetFirst(Func<T, bool> predicate);

        /// <summary>
        /// Get nested table records
        /// </summary>
        /// <param name="include">Entity seperated by comma</param>
        /// <returns>IQueryable Entity</returns>
        IQueryable<T> GetWithInclude(params string[] include);

        /// <summary>
        /// To do pagination with predicate
        /// </summary>
        /// <param name="predicate">Conditions</param>
        /// <param name="pageIndex">Index of the page</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="totalItemCount">Total Row Counts</param>
        /// <returns>Queryable items</returns>
        IQueryable<T> ToPagedList(Func<T, bool> predicate, int? pageIndex, int pageSize, out int totalItemCount);

        /// <summary>
        /// To do pagination without predicate
        /// </summary>
        /// <param name="pageIndex">Index of the page</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="totalItemCount">Total Row Counts</param>
        /// <returns>Queryable items</returns>
        IQueryable<T> ToPagedList(int? pageIndex, int pageSize, out int totalItemCount);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="exp"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        U Get<U>(Expression<Func<T, bool>> exp, Expression<Func<T, U>> columns)
        where U : class;

        /// <summary>
        /// Get Enumerable records
        /// </summary>
        /// <param name="where">Conditions</param>
        /// <returns>IEnumerable values</returns>
        IEnumerable<U> GetMany<U>(Expression<Func<T, bool>> where, Expression<Func<T, U>> columns) where U : class;
    }

}
