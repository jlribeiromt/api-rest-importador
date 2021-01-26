using System;

namespace ApiRestImportador.Domain.DTO
{
    public class ImportacaoDTO
    {
        public int ImportacaoId { get; set; }
        public int TotalItens { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataImportacao { get; set; }
    }
}
