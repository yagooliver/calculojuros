using System.Collections.Generic;
using System.Linq;
using CalculaJuros.Domain.Shared.Handler;
using CalculaJuros.Domain.Shared.Notificacao;
using CalculaJuros.Domain.Shared.Retornos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalculoJuros.CalculoApi.Api
{
    public class ApiBase : Controller
    {
        protected readonly NotificacaoDominioHandler _notificacoes;
        protected readonly IMediatorHandler _mediator;

        protected ApiBase(INotificationHandler<NotificacaoDominio> notificacoes,
                                IMediatorHandler mediator)
        {
            _notificacoes = (NotificacaoDominioHandler)notificacoes;
            _mediator = mediator;
        }

        protected IEnumerable<NotificacaoDominio> Notifications => _notificacoes.GetNotificacoes();

        protected bool OperacaoValida()
        {
            return (!_notificacoes.HasNotifications());
        }

        protected new IActionResult Response<T>(T result)
        {
            if (OperacaoValida())
            {
                return Ok(new ApiOkReturn<T>
                {
                    success = true,
                    valor = result
                });
            }

            return BadRequest(new ApiBadReturn
            {
                success = false,
                errors = _notificacoes.GetNotificacoes().Select(n => n.Valor)
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
            _mediator.RaiseEvent(new NotificacaoDominio(code, message));
        }
    }
}
