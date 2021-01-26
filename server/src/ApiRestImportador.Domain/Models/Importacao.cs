using System;
using System.Collections.Generic;

namespace ApiRestImportador.Domain.Models
{
    public class Importacao
    {
        // EF Core
        protected Importacao()
        {
            ItemImportacaos = new HashSet<ItemImportacao>();
        }

        public Importacao(int totalItens, 
                          decimal valorTotal, 
                          DateTime dataImportacao)
        {
            TotalItens = totalItens;
            ValorTotal = valorTotal;
            DataImportacao = dataImportacao;
            ItemImportacaos = new HashSet<ItemImportacao>();
        }

        public int ImportacaoId { get; private set; }
        public int TotalItens { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataImportacao { get; private set; }

        public virtual ICollection<ItemImportacao> ItemImportacaos { get; protected set; }

        public void AdicionnarItensImportacao(ItemImportacao item)
        {
            ItemImportacaos.Add(item);
        }
    }
}
