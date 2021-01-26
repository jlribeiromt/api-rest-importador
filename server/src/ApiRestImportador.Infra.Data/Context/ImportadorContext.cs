using ApiRestImportador.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiRestImportador.Infra.Data.Context
{
    public class ImportadorContext : DbContext
    {
        public ImportadorContext(DbContextOptions<ImportadorContext> options)
        : base(options)
        { }

        public DbSet<Importacao> Importacao { get; set; }
        public DbSet<ItemImportacao> ItemImportacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
