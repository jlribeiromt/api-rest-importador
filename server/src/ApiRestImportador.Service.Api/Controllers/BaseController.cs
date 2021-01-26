using ApiRestImportador.Domain.Core.Bus;
using ApiRestImportador.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ApiRestImportador.Service.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediator;

        protected BaseController(INotificationHandler<DomainNotification> notifications,
                                 IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.RaiseEvent(new DomainNotification(code, message));
        }
    }
}
