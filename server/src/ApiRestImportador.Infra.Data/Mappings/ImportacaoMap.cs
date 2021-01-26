using ApiRestImportador.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiRestImportador.Infra.Data.Mappings
{
    public class ImportacaoMap : IEntityTypeConfiguration<Importacao>
    {
        public void Configure(EntityTypeBuilder<Importacao> builder)
        {
            builder.Property(e => e.DataImportacao).HasColumnType("date");

            builder.Property(e => e.ValorTotal).HasColumnType("decimal(15, 2)");
        }
    }
}
