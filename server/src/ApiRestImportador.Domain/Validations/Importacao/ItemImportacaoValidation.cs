using ApiRestImportador.Domain.Commands.Importacao;
using FluentValidation;
using System;

namespace ApiRestImportador.Domain.Validacoes.Importacao
{
    public class ItemImportacaoValidation : AbstractValidator<ItemImportacaoCommand>
    {
        public ItemImportacaoValidation()
        {
            RuleFor(p => p.DataEntrega)
                .NotEmpty()
                .WithMessage("O campo Data entrega é requerida.")
                .Must(DataEntragaValidacao)
                .WithMessage(x => $"Linha:{x.Linha} Data de entrega não pode ser menor ou igual a hoje.");

            RuleFor(p => p.NomeProduto)
                .NotEmpty()
                .WithMessage(x => $"Linha:{x.Linha} O campo Nome é requerido.")
                .Length(2, 50)
                .WithMessage(x => $"Linha:{x.Linha} O campo Nome deve ter entre 2 e 50 caracteres.");

            RuleFor(p => p.ValorUnitario)
               .NotEmpty()
               .WithMessage(x => $"Linha:{x.Linha} O campo Valor unitário é requerido.");

            RuleFor(p => p.Quantidade)
            .NotEmpty()
            .WithMessage(x => $"Linha:{x.Linha} O campo Quantidade é requerido.");
        }

        private bool DataEntragaValidacao(DateTime data)
        {
            return !(data <= DateTime.Today);
        }
    }
}
