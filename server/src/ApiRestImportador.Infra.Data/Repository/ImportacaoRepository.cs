using ApiRestImportador.Domain.Interfaces.Repository;
using ApiRestImportador.Domain.Models;
using ApiRestImportador.Infra.Data.Context;

namespace ApiRestImportador.Infra.Data.Repository
{
    public class ImportacaoRepository : Repository<Importacao>, IImportacaoRepository
    {
        public ImportacaoRepository(ImportadorContext dbContext) 
            : base(dbContext)
        {}
    }
}
