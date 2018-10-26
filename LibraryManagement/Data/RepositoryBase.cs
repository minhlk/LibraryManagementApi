using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LibraryManagement.Data.Interface;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace LibraryManagement.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected LibraryManagementContext RepositoryContext { get; set; }

        protected RepositoryBase(LibraryManagementContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await this.RepositoryContext.Set<T>().ToListAsync();
        }
        public async Task<int> CountAllAsync()
        {
            return await this.RepositoryContext.Set<T>().CountAsync();
        }
        public async Task<IEnumerable<T>> FindAllByPageAsync(int page, int numPerPage,params Expression<Func<T,object>>[] expressions)
        {
            IQueryable<T> query = null;
            query = this.RepositoryContext.Set<T>();
            foreach (var expression in expressions)
            {
                query = query.Include(expression);
            }
            return await query.Skip(page * numPerPage).Take(numPerPage).ToListAsync();   
        }
        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.RepositoryContext.Set<T>().Where(expression).ToListAsync();
        }

        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public async Task SaveAsync()
        {
            await this.RepositoryContext.SaveChangesAsync();
        }
    }

}
