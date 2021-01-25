using MediatR;
using System;

namespace ApiRestImportador.Domain.Core.Events
{
    public abstract class Event : Message, INotification
    {
        public DateTime TimeStamp { get; private set; }

        protected Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
