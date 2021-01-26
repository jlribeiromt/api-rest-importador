using ApiRestImportador.Domain.Core.Commands;
using System;
using System.Collections.Generic;

namespace ApiRestImportador.Domain.Commands.Importacao
{
    public abstract class ImportacaoCommand : Command
    {
        public int ImportacaoId { get; protected set; }
        public int TotalItens { get; protected set; }
        public decimal ValorTotal { get; protected set; }
        public DateTime DataImportacao { get; protected set; }

        public List<ItemImportacaoCommand> ItemImportacaos { get; protected set; }
    }
}
