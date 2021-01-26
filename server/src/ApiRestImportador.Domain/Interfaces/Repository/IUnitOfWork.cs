using System;

namespace ApiRestImportador.Domain.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
