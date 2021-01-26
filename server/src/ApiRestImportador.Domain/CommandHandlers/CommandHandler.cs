using ApiRestImportador.Domain.Core.Bus;
using ApiRestImportador.Domain.Core.Commands;
using ApiRestImportador.Domain.Core.Notifications;
using ApiRestImportador.Domain.Interfaces.Repository;
using MediatR;

namespace ApiRestImportador.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        public bool Commit()
        {
            if (_notifications.HasNotifications())
                return false;

            if (_uow.Commit())
                return true;

            _bus.RaiseEvent(new DomainNotification("Commit", "Tivemos um problema ao salvar seus dados."));
            return false;
        }
    }
}
