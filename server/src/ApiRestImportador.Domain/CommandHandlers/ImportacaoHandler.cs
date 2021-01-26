using ApiRestImportador.Domain.Commands.Importacao;
using ApiRestImportador.Domain.Core.Bus;
using ApiRestImportador.Domain.Core.Notifications;
using ApiRestImportador.Domain.Interfaces.Repository;
using ApiRestImportador.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiRestImportador.Domain.CommandHandlers
{
    public class ImportacaoHandler : CommandHandler,
        IRequestHandler<NewImportacaoCommand, bool>
    {
        private readonly IImportacaoRepository _repository;


        public ImportacaoHandler(IImportacaoRepository repository,
                                 IUnitOfWork uow,
                                 IMediatorHandler bus,
                                 INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _repository = repository;
        }

        public Task<bool> Handle(NewImportacaoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var importacao = new Importacao(request.TotalItens, request.ValorTotal, request.DataImportacao);
            request.ItemImportacaos.ForEach(item =>
            importacao.AdicionnarItensImportacao(new ItemImportacao(item.NomeProduto, item.DataEntrega, item.Quantidade, item.ValorUnitario)));
            _repository.Add(importacao);
            Commit();

            return Task.FromResult(true);
        }
    }
}
