using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Interface
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
//        Task<IEnumerable<T>> FindAllByPageAsync(int page, int numPerPage, params Expression<Func<T, object>>[] expressions);
        Task<int> CountAllAsync();
        IQueryable<T> CountAll(Expression<Func<T, bool>> expression);
    }
}