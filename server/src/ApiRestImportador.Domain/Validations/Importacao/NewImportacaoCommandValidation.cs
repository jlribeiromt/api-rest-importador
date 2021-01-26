using ApiRestImportador.Domain.Commands.Importacao;
using ApiRestImportador.Domain.Validacoes.Importacao;

namespace ApiRestImportador.Domain.Validations.Importacao
{
    public class NewImportacaoCommandValidation : ImportacaoValidation<NewImportacaoCommand>
    {
        public NewImportacaoCommandValidation()
        {
            ValidarDataImportacao();
            ValidarTotalItens();
            ValidarValorTotal();
            ValidarItensImportacao();
        }
    }
}
