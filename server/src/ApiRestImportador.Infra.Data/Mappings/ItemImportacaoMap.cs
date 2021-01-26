using ApiRestImportador.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiRestImportador.Infra.Data.Mappings
{
    public class ItemImportacaoMap : IEntityTypeConfiguration<ItemImportacao>
    {
        public void Configure(EntityTypeBuilder<ItemImportacao> builder)
        {
            builder.Property(e => e.DataEntrega).HasColumnType("date");

            builder.Property(e => e.NomeProduto)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.ValorUnitario).HasColumnType("decimal(15, 2)");

            builder.HasOne(d => d.Importacao)
                .WithMany(p => p.ItemImportacaos)
                .HasForeignKey(d => d.ImportacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Importacao_ImportacaoId_ItemImportacao_ImportacaoId");
        }
    }
}
