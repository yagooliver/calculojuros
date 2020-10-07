using CalculaJuros.Domain.Shared.Command;
using CalculaJuros.Domain.Shared.Handler;
using MediatR;
using System.Threading.Tasks;

namespace CalculaJuros.CrossCutting.Bus
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task RaiseEvent<T>(T @event) where T : class
        {
            await _mediator.Publish(@event);
        }

        public async Task<TResult> SendCommandResult<TResult>(ICommandResult<TResult> command)
        {
            return await _mediator.Send(command);
        }
    }
}
