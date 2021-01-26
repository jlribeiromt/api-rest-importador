using ApiRestImportador.Domain.Interfaces.Repository;
using ApiRestImportador.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiRestImportador.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ImportadorContext _dbContext;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ImportadorContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<TEntity>();

        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public virtual TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression != null)
                return DbSet.Where(expression);
            else
                return DbSet;
        }

        public Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> select,
                                                  Expression<Func<TEntity, bool>> expression = null) where TResult : class
        {
            
            if (expression != null)
                return DbSet.Where(expression).Select(select).ToListAsync();
            else
                return DbSet.Select(select).ToListAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
