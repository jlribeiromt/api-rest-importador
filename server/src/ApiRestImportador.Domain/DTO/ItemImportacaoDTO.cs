using System;

namespace ApiRestImportador.Domain.DTO
{
    public class ItemImportacaoDTO
    {
        public int Linha { get; set; }
        public int ItemImportacaoId { get; set; }
        public int ImportacaoId { get; set; }
        public string NomeProduto { get; set; }
        public DateTime DataEntrega { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

        public decimal ValorTotal => Quantidade * ValorUnitario;
    }
}
