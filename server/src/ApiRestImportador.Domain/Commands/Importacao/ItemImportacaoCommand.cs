using System;

namespace ApiRestImportador.Domain.Commands.Importacao
{
    public class ItemImportacaoCommand
    {
        public ItemImportacaoCommand(int linha, string nomeProduto, DateTime dataEntrega, int quantidade, decimal valorUnitario)
        {
            Linha = linha;
            NomeProduto = nomeProduto?.Trim();
            DataEntrega = dataEntrega;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public int Linha { get; protected set; }
        public int ItemImportacaoId { get; protected set; }
        public string NomeProduto { get; private set; }
        public DateTime DataEntrega { get; protected set; }
        public int Quantidade { get; protected set; }
        public decimal ValorUnitario { get; protected set; }

        public decimal ValorTotal => Quantidade * ValorUnitario;        
    }
}
