using CalculaJuros.Domain.Shared.Command;
using System.Threading.Tasks;

namespace CalculaJuros.Domain.Shared.Handler
{
    public interface IMediatorHandler
    {
        Task<TResult> SendCommandResult<TResult>(ICommandResult<TResult> command);
        Task RaiseEvent<T>(T @event) where T : class;
    }
}
