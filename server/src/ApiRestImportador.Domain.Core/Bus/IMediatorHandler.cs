using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiRestImportador.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
