using CalculaJuros.Domain.Core.Commands;
using FluentValidation;

namespace CalculaJuros.Domain.Core.Validacao
{
    internal class CalculaJurosValidacao : AbstractValidator<CalculaJurosCommand>
    {
        public CalculaJurosValidacao()
        {
            Inicializa();
        }

        private void Inicializa()
        {
            RuleFor(x => x.Valor).GreaterThan(0).WithMessage("Valor deve ser informado e não pode ser zero");

            RuleFor(x => x.Meses).GreaterThan(1).WithMessage("Quantidade de meses deve ser informado");
        }
    }
}
