using CalculaJuros.Domain.Core.Commands;
using CalculaJuros.Domain.Shared.Handler;
using CalculaJuros.Domain.Shared.HttpHelper;
using CalculaJuros.Domain.Shared.Notificacao;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CalculaJuros.Domain.Core.Handler
{
    public class CalculaJurosCommandHandler : IRequestHandler<CalculaJurosCommand, decimal>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IHttpClientCaller _httpClientCaller;

        public CalculaJurosCommandHandler(IMediatorHandler mediatorHandler, IHttpClientCaller httpClientCaller)
        {
            _mediatorHandler = mediatorHandler;
            _httpClientCaller = httpClientCaller;
        }

        public Task<decimal> Handle(CalculaJurosCommand request, CancellationToken cancellationToken)
        {
            if (request.EhValido())
            {
                var erros = new Dictionary<string,string>();
                var jurosPercentual = _httpClientCaller.GetTaxaJuros(erros).Result;
                if (jurosPercentual == 0 && erros.Any())
                    foreach(var erro in erros)
                        _mediatorHandler.RaiseEvent(new NotificacaoDominio(erro.Key, erro.Value));
                else
                {
                    var potencia = Math.Pow(1 + jurosPercentual, request.Meses);
                    var montante = (double)request.Valor * potencia;

                    return Task.FromResult((decimal)Math.Truncate(100 * montante) / 100);
                }
            }
            else
                foreach (var erro in request.GetErrors())
                    _mediatorHandler.RaiseEvent(new NotificacaoDominio(erro.ErrorCode, erro.ErrorMessage));

            return Task.FromResult(0.0m);
        }
    }
}
