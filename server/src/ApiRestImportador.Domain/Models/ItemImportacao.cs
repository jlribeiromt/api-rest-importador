using System;

namespace ApiRestImportador.Domain.Models
{
    public class ItemImportacao
    {
        // EF Core
        protected ItemImportacao()
        { }

        public ItemImportacao(int itemImportacaoId,
                              string nomeProduto,
                              DateTime dataEntrega,
                              int quantidade,
                              decimal valorUnitario)
        {
            ItemImportacaoId = itemImportacaoId;
            NomeProduto = nomeProduto;
            DataEntrega = dataEntrega;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public ItemImportacao(string nomeProduto,
                             DateTime dataEntrega,
                             int quantidade,
                             decimal valorUnitario)
        {            
            NomeProduto = nomeProduto;
            DataEntrega = dataEntrega;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }


        public int ItemImportacaoId { get; private set; }
        public int ImportacaoId { get; private set; }
        public string NomeProduto { get; private set; }
        public DateTime DataEntrega { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public virtual Importacao Importacao { get; protected set; }
    }
}
