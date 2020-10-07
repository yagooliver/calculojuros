using MediatR;

namespace CalculaJuros.Domain.Shared.Command
{
    public interface ICommandResult<T> : IRequest<T>
    {
        bool EhValido();
    }
}
