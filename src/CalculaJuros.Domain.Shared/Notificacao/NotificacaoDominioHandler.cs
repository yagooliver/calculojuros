using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CalculaJuros.Domain.Shared.Notificacao
{
    public class NotificacaoDominioHandler : INotificationHandler<NotificacaoDominio>
    {
        private List<NotificacaoDominio> _notificacoes;

        public NotificacaoDominioHandler()
        {
            _notificacoes = new List<NotificacaoDominio>();
        }
        public Task Handle(NotificacaoDominio notification, CancellationToken cancellationToken)
        {
            _notificacoes.Add(notification);

            return Task.CompletedTask;
        }

        public virtual List<NotificacaoDominio> GetNotificacoes()
        {
            return _notificacoes;
        }

        public virtual bool HasNotifications()
        {
            return GetNotificacoes().Any();
        }

        public void Dispose()
        {
            _notificacoes = new List<NotificacaoDominio>();
        }

    }
}
