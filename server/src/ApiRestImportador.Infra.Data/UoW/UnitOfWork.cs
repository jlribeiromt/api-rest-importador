using ApiRestImportador.Domain.Interfaces.Repository;
using ApiRestImportador.Infra.Data.Context;

namespace ApiRestImportador.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ImportadorContext _context;

        public UnitOfWork(ImportadorContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
