using ApiRestImportador.Domain.Commands.Importacao;
using FluentValidation;

namespace ApiRestImportador.Domain.Validacoes.Importacao
{
    public class ImportacaoValidation<T> : AbstractValidator<T> where T : ImportacaoCommand
    {
        protected void ValidarDataImportacao()
        {
            RuleFor(p => p.DataImportacao)
                .NotEmpty()
                .WithMessage("O campo Data importação é requerida.");
        }

        protected void ValidarTotalItens()
        {
            RuleFor(p => p.TotalItens)
                .NotEmpty()
                .WithMessage("O campo Total itens é requerido.");
        }

        protected void ValidarValorTotal()
        {
            RuleFor(p => p.ValorTotal)
                .NotEmpty()
                .WithMessage("O campo Valor total é requerido.");
        }

        public void ValidarItensImportacao()
        {
            RuleForEach(x => x.ItemImportacaos).SetValidator(new ItemImportacaoValidation());
        }
    }
}
