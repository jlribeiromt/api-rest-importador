using ApiRestImportador.Domain.Core.Commands;
using ApiRestImportador.Domain.Core.Events;
using System.Threading.Tasks;

namespace ApiRestImportador.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
