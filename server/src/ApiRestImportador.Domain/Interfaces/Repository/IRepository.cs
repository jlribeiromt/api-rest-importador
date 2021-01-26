using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiRestImportador.Domain.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(int id);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null);
        Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> select,
                                        Expression<Func<TEntity, bool>> expression = null) where TResult : class;
    }
}
