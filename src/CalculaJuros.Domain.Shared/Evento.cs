using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculaJuros.Domain.Shared
{
    public abstract class Evento : IRequest<bool>, INotification
    {
        protected Evento()
        {
            DataHora = DateTime.Now;
        }

        public DateTime DataHora { get; private set; }

    }
}
