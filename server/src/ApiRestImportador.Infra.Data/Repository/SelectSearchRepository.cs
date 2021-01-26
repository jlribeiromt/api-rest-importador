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
    public class SelectSearchRepository<TEntity> : ISelectSearchRepository<TEntity> where TEntity : class
    {
        protected readonly ImportadorContext _dbContext;
        protected readonly DbSet<TEntity> DbSet;

        public SelectSearchRepository(ImportadorContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<TEntity>();

        }

        public Task<List<TResult>> GetAll<TResult>(Expression<Func<TEntity, TResult>> select, 
                                                   Expression<Func<TEntity, bool>> expression = null)
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
