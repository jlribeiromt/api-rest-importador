using ApiRestImportador.Domain.Validacoes.Importacao;
using ApiRestImportador.Domain.Validations.Importacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiRestImportador.Domain.Commands.Importacao
{
    public class NewImportacaoCommand : ImportacaoCommand
    {
        public NewImportacaoCommand()
        {          
            ItemImportacaos = new List<ItemImportacaoCommand>();
        }

        public void AdicionarItens(List<ItemImportacaoCommand> itensImportacao)
        {
            ItemImportacaos.AddRange(itensImportacao);

            DataImportacao = DateTime.Now;
            TotalItens = ItemImportacaos?.Count ?? 0;
            ValorTotal = ItemImportacaos?.Sum(x => x.ValorTotal) ?? 0;
        }

        public override bool IsValid()
        {
            ValidationResult = new NewImportacaoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
