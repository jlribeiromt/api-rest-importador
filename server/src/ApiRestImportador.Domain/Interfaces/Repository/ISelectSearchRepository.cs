using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiRestImportador.Domain.Interfaces.Repository
{
    public interface ISelectSearchRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<List<TResult>> GetAll<TResult>(Expression<Func<TEntity, TResult>> select, 
                                            Expression<Func<TEntity, bool>> expression = null);
    }
}
